using System.Net.Http.Headers;
using System.Text.Json;
using AIDentify.DTO;
using AIDentify.Models;
using AIDentify.Models.Context;
using AIDentify.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

namespace AIDentify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelsController : ControllerBase
    {

        private readonly HttpClient _httpClient;
        private readonly ContextAIDentify context;

        public ModelsController(HttpClient httpClient, ContextAIDentify context)
        {
            _httpClient = httpClient;
            this.context = context;
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

            var xRayScan = new XRayScan
            {
                ScanId = "SCAN-" + Guid.NewGuid().ToString("N").Substring(0, 8),
                ImagePath = request.File.FileName,
                ScanDate = DateTime.UtcNow
            };
            context.XRayScan.Add(xRayScan);
            await context.SaveChangesAsync();

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
                    context.XRayScan.Update(xRayScan);
                    await context.SaveChangesAsync();
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

            var xRayScan = new XRayScan
            {
                ScanId = "SCAN-" + Guid.NewGuid().ToString("N").Substring(0, 8),
                ImagePath = request.File.FileName,
                ScanDate = DateTime.UtcNow
            };
            context.XRayScan.Add(xRayScan);
            await context.SaveChangesAsync();

            var response = await _httpClient.PostAsync(pythonApiUrl, form);
            if (!response.IsSuccessStatusCode)
            {
                var errMsg = await response.Content.ReadAsStringAsync();
                return BadRequest($"Error calling the Gender prediction API: {errMsg}");
            }
            var responseString = await response.Content.ReadAsStringAsync();

            var predictionResult = JsonSerializer.Deserialize<Dictionary<string, string>>(responseString);
            if (predictionResult == null || !predictionResult.ContainsKey("gender"))
            {
                return BadRequest("Invalid response from AI model.");
            }
            if (Enum.TryParse(predictionResult["gender"], out Gender predictedGender))
            {
                xRayScan.PredictedGenderGroup = predictedGender;
                context.XRayScan.Update(xRayScan);
                await context.SaveChangesAsync();
            }
            else
            {
                return BadRequest("Failed to parse Gender prediction.");

            }
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

            var xRayScan = new XRayScan
            {
                ScanId = "SCAN-" + Guid.NewGuid().ToString("N").Substring(0, 8),
                ImagePath = request.File.FileName,
                ScanDate = DateTime.UtcNow
            };
            context.XRayScan.Add(xRayScan);
            await context.SaveChangesAsync();

            var response = await _httpClient.PostAsync(pythonApiUrl, form);
            if (!response.IsSuccessStatusCode)
            {
                var errMsg = await response.Content.ReadAsStringAsync();
                return BadRequest($"Error calling the Teeth prediction API: {errMsg}");
            }
            var responseString = await response.Content.ReadAsStringAsync();

            xRayScan.TeethPrediction = responseString;
            context.XRayScan.Update(xRayScan);
            await context.SaveChangesAsync();

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

            var xRayScan = new XRayScan
            {
                ScanId = "SCAN-" + Guid.NewGuid().ToString("N").Substring(0, 8),
                ImagePath = request.File.FileName,
                ScanDate = DateTime.UtcNow,
                DiseasePrediction = resultBytes
            };

            context.XRayScan.Add(xRayScan);
            await context.SaveChangesAsync();

            return File(resultBytes, "image/jpeg");
        }



    }
    }

