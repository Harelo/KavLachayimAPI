using Microsoft.AspNetCore.Mvc;
using KavLachayimAPI.Services;
using System.Data.SQLite;

namespace KavLachayimAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/database/{appVersion:int}")]
    public class DatabaseController : Controller
    {
        [HttpGet("version")]
        public IActionResult GetDatabaseVersion(int appVersion)
        {
            string fullDBPath = App.WebRootPath + $@"\Files\Database\V{appVersion}\KavLachayimDB.db3";
            if (System.IO.File.Exists(fullDBPath))
            {
                var dbVer = int.Parse(DBService.ExecuteSQLScalar("SELECT Version FROM DatabaseInformation LIMIT 1", new SQLiteConnection("Data Source=" + fullDBPath + ";Version=3")).ToString());
                return Ok(dbVer);
            }

            return new BadRequestResult();
        }

        [HttpGet("download")]
        public IActionResult DownloadDatabase(int appVersion)
        {
            string fullDBPath = App.WebRootPath + $@"\Files\Database\V{appVersion}\KavLachayimDB.db3";
            if (System.IO.File.Exists(fullDBPath))
                return File($"~/Files/Database/V{appVersion}/KavLachayimDB.db3", "application/db3", "KavLachayimDB.db3");

            return new BadRequestResult();
        }
    }
}
