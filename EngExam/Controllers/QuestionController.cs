using Application.DTOs.Requests;
using Application.DTOs.Requests.Question;
using Application.DTOs.Responses;
using Application.Interface;
using Application.Repositories;
using AutoMapper;
using Domain.Entity;
using EngExam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StackExchange.Redis;


namespace EngExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionManager _questionManager;
        private readonly IDistributedCache _redisDatabase;
        private const string RedisKeyPrefix = "QuestionCache";
        private readonly IMapper _mapper;
        public QuestionController(IQuestionManager questionManager, IDistributedCache connectionMultiplexer, IMapper mapper)
        {
            _questionManager = questionManager;
            _redisDatabase = connectionMultiplexer;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string cacheKey = $"{RedisKeyPrefix}:All";
            var cachedData = await _redisDatabase.GetStringAsync(cacheKey);
            if (cachedData != null)
            {
                var questionsFromCache = JsonConvert.DeserializeObject<IEnumerable<QuestionResponse>>(cachedData);
                return Ok(questionsFromCache);
            }
            var data = await _questionManager.GetAllAsync();
            var cache = JsonConvert.SerializeObject(data);
            await _redisDatabase.SetStringAsync(cacheKey, cache);
            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateQuestionRequest question)
        {
            if (question == null)
            {
                return BadRequest("Question cannot be null");
            }
            await _questionManager.AddQuestionAsync(question);
            // Invalidate cache
            await _redisDatabase.RemoveAsync($"{RedisKeyPrefix}:All");
            return CreatedAtAction(nameof(Get), question);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            string cacheKey = $"{RedisKeyPrefix}:{id}";
            var cachedData = await _redisDatabase.GetStringAsync(cacheKey);
            if (cachedData != null)
            {
                var questionFromCache = JsonConvert.DeserializeObject<QuestionResponse>(cachedData);
                return Ok(questionFromCache);
            }
            var question = await _questionManager.GetByIdAsync(id);
            if (question == null)
            {
                return NotFound();
            }
            var cache = JsonConvert.SerializeObject(question);
            await _redisDatabase.SetStringAsync(cacheKey, cache);
            return Ok(question);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _questionManager.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }
            // Invalidate cache
            await _redisDatabase.RemoveAsync($"{RedisKeyPrefix}:{id}");
            await _redisDatabase.RefreshAsync($"{RedisKeyPrefix}:All");
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] UpdateQuestionRequest question)
        {
            var existingQuestion = await _questionManager.GetByIdAsync(question.Id);
            if (existingQuestion == null)
            {
                return NotFound();
            }
            await _questionManager.UpdateQuestionAsync(question);
            // Invalidate cache
            await _redisDatabase.RemoveAsync($"{RedisKeyPrefix}:{question.Id}");
            await _redisDatabase.RemoveAsync($"{RedisKeyPrefix}:All");
            return Ok(question);
        }
    }
}
