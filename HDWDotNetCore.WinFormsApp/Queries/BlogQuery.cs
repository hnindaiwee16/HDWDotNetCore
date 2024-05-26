using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDWDotNetCore.WinFormsApp.Queries
{
    public class BlogQuery
    {
        public static string BlogCreate { get; } = @"INSERT INTO [dbo].[dotNet]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor       
           ,@BlogContent)";
    }
}
