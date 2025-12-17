using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;

namespace Singleton;

// 1. SINGLE RESPONSIBILITY - Logger ONLY does logging
public interface ILogger
{
    void Log(string message);
    void LogError(string error);
}

// 2. BEST PRACTICE: Using Lazy<T> for thread safety
public sealed class Logger : ILogger
{
    // Lazy initialization - thread-safe by default
    private static readonly Lazy<Logger> instance =
        new Lazy<Logger>(() => new Logger());

    // Queue for thread-safe logging
    private readonly ConcurrentQueue<string> logQueue = new ConcurrentQueue<string>();
    private readonly string logFilePath;

    // Private constructor
    private Logger()
    {
        logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "app.log");
        Console.WriteLine($"Logger initialized. Log file: {logFilePath}");

        // Start background thread for writing logs
        ThreadPool.QueueUserWorkItem(WriteLogsToFile);
    }

    // Public accessor
    public static Logger Instance => instance.Value;

    // Log methods
    public void Log(string message)
    {
        string logEntry = $"[INFO] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}";
        logQueue.Enqueue(logEntry);
        Console.WriteLine(logEntry);
    }

    public void LogError(string error)
    {
        string logEntry = $"[ERROR] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {error}";
        logQueue.Enqueue(logEntry);
        Console.WriteLine(logEntry);
    }

    // Background method to write logs to file
    private void WriteLogsToFile(object state)
    {
        while (true)
        {
            if (logQueue.TryDequeue(out string logEntry))
            {
                try
                {
                    File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to write log: {ex.Message}");
                }
            }
            Thread.Sleep(100); // Small delay to prevent CPU spinning
        }
    }
}

// 3. ALTERNATIVE: Dependency Injection with Singleton (Modern approach)
public class OrderService
{
    private readonly ILogger logger;

    // Dependency is injected, not created internally
    public OrderService(ILogger logger)
    {
        this.logger = logger;
    }

    public void ProcessOrder(string orderId)
    {
        logger.Log($"Processing order: {orderId}");
        // Process order logic...
        logger.Log($"Order {orderId} processed successfully");
    }
}

// 4. BETTER APPROACH: Using .NET Core's built-in Dependency Injection
public class EmailService
{
    private readonly ILogger logger;

    public EmailService(ILogger logger)
    {
        this.logger = logger;
    }

    public void SendEmail(string to)
    {
        logger.Log($"Sending email to: {to}");
        // Email sending logic...
        logger.Log($"Email sent to: {to}");
    }
}

// 5. MAIN PROGRAM - Shows different usage patterns
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== SINGLETON BEST PRACTICES DEMO ===\n");

        // Pattern 1: Direct Singleton usage (when appropriate)
        Console.WriteLine("1. DIRECT SINGLETON USAGE:");
        Console.WriteLine("---------------------------");

        Logger.Instance.Log("Application started");

        // Simulate multiple components using the same logger
        Task1();
        Task2();

        Logger.Instance.LogError("Simulated error occurred");

        // Pattern 2: Dependency Injection pattern
        Console.WriteLine("\n2. DEPENDENCY INJECTION PATTERN:");
        Console.WriteLine("--------------------------------");

        var orderService = new OrderService(Logger.Instance);
        orderService.ProcessOrder("ORD12345");

        var emailService = new EmailService(Logger.Instance);
        emailService.SendEmail("customer@example.com");

        // Pattern 3: Thread-safe demonstration
        Console.WriteLine("\n3. THREAD-SAFE DEMONSTRATION:");
        Console.WriteLine("-----------------------------");

        // Multiple threads logging simultaneously
        Thread[] threads = new Thread[5];
        for (int i = 0; i < threads.Length; i++)
        {
            int threadId = i + 1;
            threads[i] = new Thread(() =>
            {
                for (int j = 0; j < 3; j++)
                {
                    Logger.Instance.Log($"Thread {threadId} - Message {j + 1}");
                    Thread.Sleep(50);
                }
            });
        }

        // Start all threads
        foreach (var thread in threads)
        {
            thread.Start();
        }

        // Wait for all threads to complete
        foreach (var thread in threads)
        {
            thread.Join();
        }

        Console.WriteLine("\nAll threads completed.");

        // Show log file location
        Console.WriteLine($"\nLog file created at: {Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "app.log")}");
        Console.WriteLine("\n=== DEMO COMPLETED ===");
    }

    static void Task1()
    {
        Logger.Instance.Log("Task 1 executing");
    }

    static void Task2()
    {
        Logger.Instance.Log("Task 2 executing");
    }
}