using HospitalManagment_V2.Mediator.Patients.CreatePatient;
using HospitalManagment_V2.Mediator.Patients.DeletePatientById;
using HospitalManagment_V2.Mediator.Patients.GetPatientById;
using HospitalManagment_V2.Mediator.Patients.UpdatePatientById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagment_V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PatientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create-patient")]
        public async Task<IActionResult> CreatePatient(CreatePatientCommand command)
        {
            var patient = await _mediator.Send(command);
            return Ok(patient);
        }

        [HttpGet("get-patient-by-id/{id}")]
        public async Task<IActionResult> GetPatientById(int id)
        {
            var patient = await _mediator.Send(new GetPatientByIdQuery(id));
            if (patient == null)
                return BadRequest("Patient Not Found");
            return Ok(patient);
        }

        [HttpDelete("delete-patient-by-id/{id}")]
        public async Task<IActionResult> DeletePatientById(int id)
        {
            await _mediator.Send(new DeletePatientByIdCommand(id));
            return Ok("Deleted Successfully");
        }

        [HttpPut("update-patient")]
        public async Task<IActionResult> UpdatePatient(UpdatePatientCommand command)
        {
            await _mediator.Send(command);
            return Ok("Updated Successfully");
        }
    }
}
