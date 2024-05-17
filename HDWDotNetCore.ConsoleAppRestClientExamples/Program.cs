// See https://aka.ms/new-console-template for more information
using HDWDotNetCore.ConsoleAppRestClientExamples;

Console.WriteLine("Hello, World!");
RestClientExample restClient = new RestClientExample();
await restClient.RunAsync();
