using Microsoft.AspNetCore.Mvc;
using IndexedFiles.DataBase;
using IndexedFiles.FileManager;
using IndexedFilesAPI.Models;
using IndexedFiles.Core.ObjectArea;

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

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            IKey key = _dataBase.Search(id);

            return Ok(new
            {
                data = key.Data
            });
        }

        [HttpPost]
        public IActionResult Post(DataBaseRequest request)
        {
            _dataBase.Insert(request.Data);

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
