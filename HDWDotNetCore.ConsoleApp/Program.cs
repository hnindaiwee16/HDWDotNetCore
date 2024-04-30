using HDWDotNetCore.ConsoleApp.AdoDotNetExamples;
using HDWDotNetCore.ConsoleApp.EFCoreExamples;

AdoDotNetExample dotnetExample = new AdoDotNetExample();
//dotnetExample.Read();
//dotnetExample.Create("title", "author","create");
//dotnetExample.Update(2,"title2", "author2", "create2");
//dotnetExample.Delete(3);
//dotnetExample.Edit(100);
//DapperExample dapper = new DapperExample();
//dapper.Run();
EFCoreExample EFCore  = new EFCoreExample();
EFCore.Run();