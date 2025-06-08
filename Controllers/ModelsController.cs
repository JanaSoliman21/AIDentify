using System.Security.Claims;
using System.Text.Json;
using AIDentify.DTO;
using AIDentify.ID_Generator;
using AIDentify.IRepositry;
using AIDentify.Models;
using AIDentify.Models.Context;
using AIDentify.Models.Enums;
using AIDentify.Repositry;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace AIDentify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ModelsController : ControllerBase
    {
        private readonly ILogger<ModelsController> _logger;
        private readonly IdGenerator id_Generator;
        private readonly HttpClient _httpClient;
        private readonly IXRayScanRepository xRayScanRepository;

        public ModelsController(HttpClient httpClient, ILogger<ModelsController> logger, IdGenerator id_Generator, IXRayScanRepository xRayScanRepository)
        {
            _httpClient = httpClient;
             this.id_Generator = id_Generator;
            _logger = logger;
            this.xRayScanRepository = xRayScanRepository;
        }

        [HttpPost("predict_Age")]
        public async Task<IActionResult> PredictAge([FromForm] ModelsDto request)
        {
            var pythonApiUrl = "https://amrgamall2003-gp-api.hf.space/api/age/detect";

            if (request.File == null || request.File.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                await request.File.CopyToAsync(ms);
                fileBytes = ms.ToArray();
            }
            var fileContent = new ByteArrayContent(fileBytes);
            fileContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("image/jpeg");

            var form = new MultipartFormDataContent();
            form.Add(fileContent, "file", request.File.FileName);

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            string userId = userIdClaim.Value;
            var userRoleClaim = User.FindFirst(ClaimTypes.Role);
            string userRole = userRoleClaim.Value;

            var xRayScan = new XRayScan
            {
                Id = id_Generator.GenerateId<XRayScan>(ModelPrefix.XRayScan),
                ImagePath = request.File.FileName,
                ScanDate = DateTime.UtcNow
            };
            
            if (userRole == "Student")
            {
                xRayScan.StudentId = userId;
            }
            if (userRole == "Doctor")
            {
                xRayScan.DoctorId = userId;

            }
            await xRayScanRepository.AddAsync(xRayScan);

            var response = await _httpClient.PostAsync(pythonApiUrl, form);
            if (!response.IsSuccessStatusCode)
            {
                var errMsg = await response.Content.ReadAsStringAsync();
                return BadRequest($"Error calling the age prediction API: {errMsg}");
            }
            var responseString = await response.Content.ReadAsStringAsync();

            var predictionResult = JsonSerializer.Deserialize<Dictionary<string, string>>(responseString);
            if (predictionResult == null || !predictionResult.ContainsKey("age_group"))
                {
                    return BadRequest("Invalid response from AI model.");
                }
                if (Enum.TryParse(predictionResult["age_group"], out Age predictedAge))
                {
                    xRayScan.PredictedAgeGroup = predictedAge;
                     await xRayScanRepository.UpdateAsync(xRayScan);
                }
                else
                {
                    return BadRequest("Failed to parse Age prediction.");

                }
                return Ok(new
                {
                    message = "Prediction stored successfully",
                    predictedAge
                });

            }

        [HttpPost("predict_Gender")]
        public async Task<IActionResult> PredictGender([FromForm] ModelsDto request)
        {
            var pythonApiUrl = "https://amrgamall2003-gp-api.hf.space/api/gender/detect";

            if (request.File == null || request.File.Length == 0)
            {
                _logger.LogError("No file uploaded.");
                return BadRequest("No file uploaded.");
            }

            _logger.LogInformation("Received file for gender prediction: {FileName}, File Size: {FileSize} bytes", request.File.FileName, request.File.Length);

            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                await request.File.CopyToAsync(ms);
                fileBytes = ms.ToArray();
            }
            var fileContent = new ByteArrayContent(fileBytes);
            fileContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("image/jpeg");

            var form = new MultipartFormDataContent();
            form.Add(fileContent, "file", request.File.FileName);

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            string userId = userIdClaim.Value;
            var userRoleClaim = User.FindFirst(ClaimTypes.Role);
            string userRole = userRoleClaim.Value;



            var xRayScan = new XRayScan
            {
                Id = id_Generator.GenerateId<XRayScan>(ModelPrefix.XRayScan),
                ImagePath = request.File.FileName,
                ScanDate = DateTime.UtcNow
            };

            if (userRole == "Student")
            {
                xRayScan.StudentId = userId;
            }
            if (userRole == "Doctor")
            {
                xRayScan.DoctorId = userId;

            }
            await xRayScanRepository.AddAsync(xRayScan);

            var response = await _httpClient.PostAsync(pythonApiUrl, form);
            if (!response.IsSuccessStatusCode)
            {
                var errMsg = await response.Content.ReadAsStringAsync();
                _logger.LogError("Error calling the Gender prediction API: {ErrorMessage}", errMsg);
                return BadRequest($"Error calling the Gender prediction API: {errMsg}");
            }

            var responseString = await response.Content.ReadAsStringAsync();
            var predictionResult = JsonSerializer.Deserialize<Dictionary<string, string>>(responseString);

            if (predictionResult == null || !predictionResult.ContainsKey("gender"))
            {
                _logger.LogError("Invalid response from AI model.");
                return BadRequest("Invalid response from AI model.");
            }

            Gender predictedGender;
            if (Enum.TryParse(predictionResult["gender"], out predictedGender))
            {
                xRayScan.PredictedGenderGroup = predictedGender;
                await xRayScanRepository.UpdateAsync(xRayScan);
            }
            else
            {
                _logger.LogError("Failed to parse Gender prediction.");
                return BadRequest("Failed to parse Gender prediction.");
            }

            _logger.LogInformation("Gender prediction successful: {PredictedGender}", predictedGender);
            return Ok(new
            {
                message = "Prediction stored successfully",
                predictedGender
            });
        }

        [HttpPost("predict_Teeth")]
        public async Task<IActionResult> PredictTeeth([FromForm] ModelsDto request)
        {
            var pythonApiUrl = "https://amrgamal11-project-api.hf.space/detect-teeth";

            if (request.File == null || request.File.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                await request.File.CopyToAsync(ms);
                fileBytes = ms.ToArray();
            }
            var fileContent = new ByteArrayContent(fileBytes);
            fileContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("image/jpeg");

            var form = new MultipartFormDataContent();
            form.Add(fileContent, "file", request.File.FileName);

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            string userId = userIdClaim.Value;
            var userRoleClaim = User.FindFirst(ClaimTypes.Role);
            string userRole = userRoleClaim.Value;

            var xRayScan = new XRayScan
            {
                Id = id_Generator.GenerateId<XRayScan>(ModelPrefix.XRayScan),
                ImagePath = request.File.FileName,
                ScanDate = DateTime.UtcNow
            };

            if (userRole == "Student")
            {
                xRayScan.StudentId = userId;
            }
            if (userRole == "Doctor")
            {
                xRayScan.DoctorId = userId;

            }
            await xRayScanRepository.AddAsync(xRayScan);

            var response = await _httpClient.PostAsync(pythonApiUrl, form);
            if (!response.IsSuccessStatusCode)
            {
                var errMsg = await response.Content.ReadAsStringAsync();
                return BadRequest($"Error calling the Teeth prediction API: {errMsg}");
            }
            var responseString = await response.Content.ReadAsStringAsync();

            xRayScan.TeethPrediction = responseString;
            await xRayScanRepository.UpdateAsync(xRayScan);

            var predictionResult = JsonSerializer.Deserialize<Dictionary<string, object>>(responseString);
            if (predictionResult == null)
            {
                return BadRequest("Invalid response from AI model.");
            }

            return Ok(new
            {
                message = "Prediction stored successfully",
                predictionResult
            });

        }

        [HttpPost("predict_Disease")]
        public async Task<IActionResult> PredictDisease([FromForm] ModelsDto request)
        {
            var pythonApiUrl = "https://amrgamal11-project-api.hf.space/detect-disease";

            if (request.File == null || request.File.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            //var existingScan = await context.XRayScan
            //    .FirstOrDefaultAsync(x => x.ImagePath == request.File.FileName);

            //if (existingScan != null)
            //{
            //    return BadRequest("This image has already been processed.");
            //}

            using var memoryStream = new MemoryStream();
            await request.File.CopyToAsync(memoryStream);
            byte[] fileBytes = memoryStream.ToArray();

            var imageContent = new ByteArrayContent(fileBytes);
            imageContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("image/jpeg");

            var form = new MultipartFormDataContent();
            form.Add(imageContent, "file", request.File.FileName);

            HttpResponseMessage response;
            try
            {
                response = await _httpClient.PostAsync(pythonApiUrl, form);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error calling the detection API: {ex.Message}");
            }

            if (!response.IsSuccessStatusCode)
            {
                var errorMsg = await response.Content.ReadAsStringAsync();
                return BadRequest($"API Error: {(int)response.StatusCode} - {errorMsg}");
            }

            var resultBytes = await response.Content.ReadAsByteArrayAsync();

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            string userId = userIdClaim.Value;
            var userRoleClaim = User.FindFirst(ClaimTypes.Role);
            string userRole = userRoleClaim.Value;

            var xRayScan = new XRayScan
            {
                Id = id_Generator.GenerateId<XRayScan>(ModelPrefix.XRayScan),
                ImagePath = request.File.FileName,
                ScanDate = DateTime.UtcNow
            };

            if (userRole == "Student")
            {
                xRayScan.StudentId = userId;
            }
            if (userRole == "Doctor")
            {
                xRayScan.DoctorId = userId;

            }
            await xRayScanRepository.AddAsync(xRayScan);

            return File(resultBytes, "image/jpeg");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await xRayScanRepository.DeleteAsync(id);
                return Ok("Model deleted successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

    }
    }

