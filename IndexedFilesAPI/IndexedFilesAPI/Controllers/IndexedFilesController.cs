using Microsoft.AspNetCore.Mvc;
using IndexedFiles.DataBase;
using IndexedFiles.FileManager;
using IndexedFilesAPI.Models;

namespace IndexedFilesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IndexedFilesController : Controller
    {
        private readonly IDataBaseHandler _dataBase;

        public IndexedFilesController() => _dataBase = FileOperator.DeserializeDataBase();

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                objectArea = _dataBase.GetObjectArea(),
                indexArea = _dataBase.GetIndexArea()
            });
        }

        [HttpPost]
        public IActionResult Post(Key key)
        {
            _dataBase.Insert(key.Data);

            return Ok(new
            {
                objectArea = _dataBase.GetObjectArea(),
                indexArea = _dataBase.GetIndexArea()
            });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _dataBase.Remove(id);

            return Ok(new
            {
                objectArea = _dataBase.GetObjectArea(),
                indexArea = _dataBase.GetIndexArea()
            });
        }
    }
}
