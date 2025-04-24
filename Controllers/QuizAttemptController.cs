using AIDentify.ID_Generator;
using AIDentify.IRepositry;
using AIDentify.Models;
using AIDentify.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace AIDentify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizAttemptController: ControllerBase
    {
        private readonly IQuizAttemptRepository _quizAttemptRepository;
        private readonly IdGenerator _idGenerator;

        public QuizAttemptController(IQuizAttemptRepository quizAttemptRepository, IdGenerator idGenerator)
        {
            _quizAttemptRepository = quizAttemptRepository;
            _idGenerator = idGenerator;
        }

        #region Get All Quiz Attempts

        [HttpGet]
        public ActionResult<List<QuizAttempt>> GetAll()
        {
            var quizAttempts = _quizAttemptRepository.GetAll();
            if (quizAttempts == null || !quizAttempts.Any())
            {
                return NotFound("No quiz attempts found.");
            }
            return Ok(quizAttempts);
        }

        #endregion
        
        #region Get Quiz Attempt by ID
        
        [HttpGet("{id}")]
        public ActionResult<QuizAttempt> Get(string id)
        {
            var quizAttempt = _quizAttemptRepository.GetById(id);
            if (quizAttempt == null)
            {
                return NotFound("Quiz attempt not found.");
            }
            return Ok(quizAttempt);
        }
        
        #endregion
        
        #region Add New Quiz Attempt
        
        [HttpPost]
        public IActionResult Add([FromBody] QuizAttempt quizAttempt)
        {
            if (quizAttempt == null)
            {
                return BadRequest("Quiz attempt cannot be null.");
            }
            quizAttempt.Id = _idGenerator.GenerateId<QuizAttempt>(ModelPrefix.QuizAttempt);
            _quizAttemptRepository.Add(quizAttempt);
            return Ok("Posted Successfully");
        }

        #endregion

        #region Update Quiz Attempt

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] QuizAttempt quizAttempt)
        {
            if (quizAttempt == null)
            {
                return BadRequest("Quiz attempt cannot be null.");
            }
            var existingQuizAttempt = _quizAttemptRepository.GetById(id);
            if (existingQuizAttempt == null)
            {
                return NotFound("Quiz attempt not found.");
            }
            quizAttempt.Id = id;
            _quizAttemptRepository.Update(quizAttempt);
            return Ok("Updated Successfully");
        }

        #endregion

        #region Delete Quiz Attempt

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var existingQuizAttempt = _quizAttemptRepository.GetById(id);
            if (existingQuizAttempt == null)
            {
                return NotFound("Quiz attempt not found.");
            }
            _quizAttemptRepository.Delete(existingQuizAttempt);
            return Ok("Deleted Successfully");
        }

        #endregion

        #region Delete All Quiz Attempts

        [HttpDelete]
        public IActionResult DeleteAll()
        {
            _quizAttemptRepository.DeleteAll();
            return Ok("All quiz attempts deleted successfully.");
        }

        #endregion
    }
}
