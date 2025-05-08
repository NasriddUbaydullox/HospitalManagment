using System.Net;
using AutoMapper;
using HospitalManagment_V2.classes;
using HospitalManagment_V2.DataAccess;
using HospitalManagment_V2.DataAccess.Entities;
using HospitalManagment_V2.Dtos;
using HospitalManagment_V2.Examples;
using HospitalManagment_V2.Mediator.Doctors.CreateDoctor;
using HospitalManagment_V2.Mediator.Doctors.GetDoctorById;
using HospitalManagment_V2.Repository;
using HospitalManagment_V2.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace HospitalManagment_V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[EnableRateLimiting("fixed")]
    public class TestController : ControllerBase
    {
        private readonly IMediator _mediatr;
        private readonly IDoctorRepository _doc;
        private readonly ILogger<TestController> _logger;
        private readonly ICorroletionId _corrId;
        private readonly Context _context;
        private readonly IMapper _mapper;
        private readonly IPatientRepository _repository;
        private readonly IPatientService _patientService;
        private readonly IPatientRepository _patientRepos;

        public TestController(ICorroletionId corroletionId, 
            ILogger<TestController> logger, Context context , 
            IMapper mapper , IPatientRepository patientRepository,
            IPatientService patientService,
            IPatientRepository patient,
            IDoctorRepository doctor,
            IMediator mediator)
        {
            _mediatr = mediator;
            _doc = doctor;
            _logger = logger;
            _corrId = corroletionId;
            _context = context;
            _mapper = mapper;
            _repository = patientRepository;
            _patientService = patientService;
            _patientRepos = patient;
        }

        [HttpGet("test")]
        public IActionResult TestCorrId()
        {
            _logger.LogInformation("CorreletionId {correletionId}", _corrId.Get());
            var getSomething = _context.Doctors.ToList();
            return Ok(getSomething);
        }

        //[HttpGet ("get-all-patient")]
        //[EnableRateLimiting("sliding")]
        //public async Task<IActionResult> GetAll()
        //{
        //    return Ok(_patientService.GetAllPatients());
        //}

        //[HttpGet ("get-patient-by-id")]


        //public async Task<IActionResult> GetById([FromRoute] int id)
        //{
        //    return Ok(await _patientRepos.GetByIdAsync(id));
        //}

        [HttpGet]
        [SwaggerOperation("Get all Doctors")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(DoctorExample))]
        [SwaggerResponse((int)HttpStatusCode.OK, "Get all menu", typeof(IList<DoctorDto>))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Menu not found")]
        public async Task<IActionResult> GetAllDoctors()
        {
            return Ok(await _doc.GetAllAsync());
        }

        [HttpGet("get-doctor-by-id-mediator/{id}")]
        public async Task<IActionResult> GetDoctorById([FromRoute] int id)
        {
            var doctor = await _mediatr.Send(new GetDoctorByIdQuery(id));
            if(doctor == null)
                return NotFound();
            return Ok();
        }

    }
}
