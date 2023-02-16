using _3SemOblOpgave1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _3SemOblOpgave_4_5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly CarsRepository _carsRepository;

        public CarsController(CarsRepository carsRepository)
        {
            _carsRepository = carsRepository;
        }

        // GET: CarsController
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public ActionResult<IEnumerable<Car>> Index()
        {
            try
            {
                List<Car> cars = _carsRepository.GetAll();
                return Ok(cars);
            }
            catch (ArgumentNullException)
            {
                return NotFound("No Cars was Found");
            }
        }

        // GET: CarsController/Details/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<Car> Details(int id)
        {
            try
            {
                Car car = _carsRepository.GetById(id);
                return Ok(car);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound($"Car with Id: {id} was not Found. " + ex.Message);
            }
        }

        // GET: CarsController/Create
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<Car> Create([FromBody] Car newCar)
        {
            try
            {
                Car car = _carsRepository.CreateCar(newCar);
                return Created($"api/cars/{car.Id}", car);
            }
            catch (ArgumentNullException)
            {
                return BadRequest("Car was not Created" + newCar);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // GET: CarsController/Edit/5
        [HttpPut("{id}")]
        public ActionResult<Car?> Edit(int id, [FromBody] Car carToUpdate)
        {


            try
            {
                Car carToBeUpdated = _carsRepository.GetById(id);
                if (carToBeUpdated == null)
                {
                    return NotFound($"No Such Car, Id: " + id);
                }
                Car carUpdated = _carsRepository.UpdateCar(carToBeUpdated.Id, carToUpdate);
                return Ok(carUpdated);
            }
            catch (ArgumentNullException)
            {
                return BadRequest("Car Not Updated, the data to update could not be processed");
            }
        }

        // GET: CarsController/Delete/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Car carToDelete = _carsRepository.GetById(id);
            if (carToDelete == null)
            {
                return NotFound($"No Such Car, Id: " + id);
            }
            _carsRepository.DeleteCar(id);
            return Ok("Car Deleted");
        }
    }
}
