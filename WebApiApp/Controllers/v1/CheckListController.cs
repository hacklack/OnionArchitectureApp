using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Features.Commands.ChecklistCommands;
using static Domain.CommonCodes.CommonEnums;
using Application.Features.Queries.ChecklistQueries;
using Application.ApiModels;

namespace WebApiApp.Controllers.v1
{
    [ApiVersion("1.0")]
    public class CheckListController : BaseApiController
    {
        #region ChecklistQuestionTypeApis
        [HttpPost("createUpdateChecklistQuestionType")]
        public async Task<IActionResult> CreateUpdateQuestionType(CreateUpdateChecklistQuestionType command)
        {
            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (Exception)
            {
                return new JsonResult(BadRequest());
            }
        }
        [HttpPost("deleteQuestionType")]
        public async Task<IActionResult> DeleteQuestionType(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new DeleteChecklistQuestionTypeCommand { Id = id }));
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("getAllQuestionTypes")]
        public async Task<IActionResult> GetAllQuestionTypes(int? checklistTypeId)
        {
            try
            {
                return Ok(await Mediator.Send(new GetAllCheckListQuestionTypeQuery { ChecklistTypeId = checklistTypeId }));

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("getQuestionTypeById/{id}")]
        public async Task<IActionResult> GetQuestionTypeId(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetCheckListQuestionTypeByIdQuery { Id = id }));
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
        #region ChecklistQuestions
        [HttpPost("createUpdateChecklist")]
        public async Task<IActionResult> CreateUpdateChecklist(CreateUpdateChecklistQuestionGenericCommand command)
        {
            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (Exception)
            {
                return new JsonResult(BadRequest());
            }
        }
        [HttpGet("getAllChecklists")]
        public async Task<IActionResult> GetAllChecklists()
        {
            try
            {
                return Ok(await Mediator.Send(new GetAllCheckListQuestionGenericQuery()));
            }
            catch (Exception)
            {
                return new JsonResult(BadRequest());
            }
        }
        [HttpGet("getAllChecklistQuestionByQuestionId")]
        public async Task<IActionResult> GetAllChecklistQuestionByQuestionId(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetCheckListQuestionByIdGenericQuery { Id = id }));
            }
            catch (Exception)
            {
                return new JsonResult(BadRequest());
            }
        }
            
        [   HttpGet("getCheckListByChecklistId")]
        public async Task<IActionResult> GetCheckListByChecklistId(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetCheckListByChecklistIdQuery { ChecklistId = id }));
            }
                catch (Exception)
            {
                return new JsonResult(BadRequest());
            }
        }
        [HttpGet("getCheckListByChecklistTypeIdAndChildId")]
        public async Task<IActionResult> GetCheckListByChecklistTypeIdAndChildId(int checklistTypeIdId, int checkListTypeChildId, int userId)
        {
            try
            {
                return Ok(await Mediator.Send(new GetCheckListByChecklistTypeIdAndChildIdQuery { ChecklistTypeId = checklistTypeIdId, CheckListTypeChildId = checkListTypeChildId, UserId = userId }));
            }
            catch (Exception)
            {
                return new JsonResult(BadRequest());
            }
        }
        [HttpPost("updateChecklistQuestionsByQuestionId")]
        public async Task<IActionResult> UpdateChecklistQuestionsByQuestionId(CreateUpdateCheckListSubjectiveAnswerQuestionCommand command)
        {
            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (Exception)
            {
                return new JsonResult(BadRequest());
            }
        }
        [HttpPost("updateChecklistQuestionsOptionByQuestionOptionId")]
        public async Task<IActionResult> UpdateChecklistQuestionsOptionByQuestionOptionId(CreateUpdateCheckListQuestionOptionCommand command)
        {
            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (Exception)
            {
                return new JsonResult(BadRequest());
            }
        }

        [HttpPost("deleteChecklistQuestionByQuestionId")]
        public async Task<IActionResult> DeleteChecklistQuestionByQuestionId(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new DeleteChecklistQuestionGenericCommand { Id = id }));
            }
            catch (Exception)   
            {

                throw;
            }
        }
        [HttpPost("deleteChecklistQuestionOptionByOptionId")]
        public async Task<IActionResult> DeleteChecklistQuestionOptionByOptionId(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new DeleteChecklistCheckListQuestionOptionCommand { Id = id }));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("deleteChecklistChecklistId")]
        public async Task<IActionResult> DeleteChecklistByChecklistId(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new DeleteChecklistCommand { Id = id }));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("createUpdateChecklistResponse")]
        public async Task<IActionResult> CreateUpdateChecklistResponse(CreateUpdateChecklistResponseCommand command)
        {
            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (Exception)
            {
                return new JsonResult(BadRequest());
            }

        }
        #endregion
        #region ChecklistUsersApis
        [HttpGet("getCheckListUsersByChecklistTypeAndChecklistChildId")]
        public async Task<IActionResult> GetCheckListUsersByChecklistTypeAndChecklistChildId(int checklistTypeIdId, int checkListTypeChildId)
        {
            try
            {
                return Ok(await Mediator.Send(new GetCheckUsersListByChecklistTypeIdAndChildIdQuery { ChecklistTypeId = checklistTypeIdId, CheckListTypeChildId = checkListTypeChildId }));
            }
            catch (Exception)
            {
                return new JsonResult(BadRequest());
            }
        }
        #endregion

    }

}
