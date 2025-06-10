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
        private readonly IQuestionRepository _questionRepository;
        private readonly IQuizRepository _quizRepository;
        private readonly IdGenerator _idGenerator;

        public QuizAttemptController(IQuizAttemptRepository quizAttemptRepository, IdGenerator idGenerator, IQuizRepository quizRepository, IQuestionRepository questionRepository)
        {
            _quizAttemptRepository = quizAttemptRepository;
            _questionRepository = questionRepository;
            _quizRepository = quizRepository;
            _idGenerator = idGenerator;
        }

        #region Normal CRUD Methods

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

        #region Get Quiz Attempts by Student ID
        [HttpGet("student/{studentId}")]
        public ActionResult<List<QuizAttempt>> GetAllMyAttempts(string studentId)
        {
            var quizAttempts = _quizAttemptRepository.GetByStudentId(studentId);
            if (quizAttempts == null || !quizAttempts.Any())
            {
                return NotFound("No quiz attempts found for the specified student.");
            }
            return Ok(quizAttempts);
        }
        #endregion

        #region Add New Quiz Attempt (Commented)

        //[HttpPost]
        //public IActionResult Add([FromBody] QuizAttempt quizAttempt)
        //{
        //    if (quizAttempt == null)
        //    {
        //        return BadRequest("Quiz attempt cannot be null.");
        //    }
        //    quizAttempt.Id = _idGenerator.GenerateId<QuizAttempt>(ModelPrefix.QuizAttempt);
        //    _quizAttemptRepository.Add(quizAttempt);
        //    return Ok("Posted Successfully");
        //}

        #endregion

        #region Update Quiz Attempt (Commented)

        //[HttpPut("{id}")]
        //public IActionResult Update(string id, [FromBody] QuizAttempt quizAttempt)
        //{
        //    if (quizAttempt == null)
        //    {
        //        return BadRequest("Quiz attempt cannot be null.");
        //    }
        //    var existingQuizAttempt = _quizAttemptRepository.GetById(id);
        //    if (existingQuizAttempt == null)
        //    {
        //        return NotFound("Quiz attempt not found.");
        //    }
        //    quizAttempt.Id = id;
        //    _quizAttemptRepository.Update(quizAttempt);
        //    return Ok("Updated Successfully");
        //}

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

        #region Delete All Quiz Attempts (Commented)

        //[HttpDelete]
        //public IActionResult DeleteAll()
        //{
        //    _quizAttemptRepository.DeleteAll();
        //    return Ok("All quiz attempts deleted successfully.");
        //}

        #endregion

        #endregion

        #region Custom Methods

        #region Start a Quiz Attempts by Student ID

        [HttpPost("start/{studentId}/{quizId}")]
        public IActionResult StartQuiz(string studentId, string quizId, [FromBody] List<string> answers)
        {
            var quiz = _quizRepository.GetById(quizId);
            if (quiz == null)
            {
                return BadRequest("There is no such a quiz.");
            }

            if (answers == null || !answers.Any())
            {
                return BadRequest("Answers cannot be null or empty.");
            }

            int points = 0;
            List<Question> questions = _questionRepository.FindByQuizId(quizId);

            for(int i = 0; i < questions.Count; i++)
            {
                if (answers[i] == questions[i].CorrectAnswer)
                {
                    points++;
                }
            }

            var quizAttempt = new QuizAttempt
            {
                Id = _idGenerator.GenerateId<QuizAttempt>(ModelPrefix.QuizAttempt),
                StudentId = studentId,
                QuizId = quizId,
                SelectedAnswers = answers,
                PointsEarned = points
            };

            // Increment total points for the student
            _quizAttemptRepository.IncrementTotalPointsForStudent(studentId, points);   // Unfinished method in repository

            _quizAttemptRepository.Add(quizAttempt);
            return Ok("Quiz attempt started successfully." + " Your points: " + points);
        }

        #endregion

        #endregion
    }
}
