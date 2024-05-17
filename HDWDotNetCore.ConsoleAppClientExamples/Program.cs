// See https://aka.ms/new-console-template for more information
using HDWDotNetCore.ConsoleAppHttpClientExamples;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

Console.WriteLine("Hello, World!");
HttpClientExample example = new HttpClientExample();
await example.RunAsync();