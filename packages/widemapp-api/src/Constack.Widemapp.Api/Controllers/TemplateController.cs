using Constack.Widemapp.Api.Controllers.Base;
using Constack.Widemapp.Application.Services.Templates.Commands.AddFile;
using Constack.Widemapp.Application.Services.Templates.Commands.AddGroup;
using Constack.Widemapp.Application.Services.Templates.Commands.AddTemplate;
using Constack.Widemapp.Application.Services.Templates.Commands.DeleteFileOrGroup;
using Constack.Widemapp.Application.Services.Templates.Commands.RenameFile;
using Constack.Widemapp.Application.Services.Templates.Commands.RenameGroup;
using Constack.Widemapp.Application.Services.Templates.Commands.SaveFile;
using Constack.Widemapp.Application.Services.Templates.Queries.GetAll;
using Constack.Widemapp.Application.Services.Templates.Queries.GetFile;
using Constack.Widemapp.Application.Services.Templates.Queries.GetGroup;
using Constack.Widemapp.Application.Services.Templates.Queries.GetTreeByGroupId;
using Constack.Widemapp.Application.Services.Templates.Queries.GetTreeView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Constack.Widemapp.Api.Controllers
{
    [Route("api/templates")]
    public class TemplateController : BaseController
    {
        [HttpGet()]
        public async Task<IActionResult> GetAllTemplates([FromQuery] GetAllTemplatesQuery query)
        {
            var result = await Mediator.Send(query);

            return Ok(result);
        }
        
        [HttpGet("tree-view")]
        public async Task<IActionResult> GetTreeView([FromQuery] GetTreeViewQuery query)
        {
            var result = await Mediator.Send(query);

            return Ok(result);
        }
        
        [HttpGet("tree-view/group")]
        public async Task<IActionResult> GetTreeViewByGropId([FromQuery] GetTreeByGroupIdQuery query)
        {
            var result = await Mediator.Send(query);

            return Ok(result);
        }
        
        [HttpGet("file")]
        public async Task<IActionResult> GetFile([FromQuery] GetFileQuery query)
        {
            var result = await Mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("group")]
        public async Task<IActionResult> GetFolder([FromQuery] GetFolderQuery query)
        {
            var result = await Mediator.Send(query);

            return Ok(result);
        }

        [HttpPost("save-file")]
        public async Task<IActionResult> SaveFile([FromBody] SaveFileCommand command)
        {
            var result = await Mediator.Send(command);

            return Ok(result);
        }
        
        [HttpPost("file")]
        public async Task<IActionResult> AddFile([FromBody] AddFileCommand command)
        {
            var result = await Mediator.Send(command);

            return Ok(result);
        }
        
        [HttpPut("file")]
        public async Task<IActionResult> UpdateFile([FromBody] RenameFileCommand command)
        {
            var result = await Mediator.Send(command);

            return Ok(result);
        }
        
        [HttpPost("group")]
        public async Task<IActionResult> AddGroup([FromBody] AddGroupCommand command)
        {
            var result = await Mediator.Send(command);

            return Ok(result);
        }
        
        [HttpPut("group")]
        public async Task<IActionResult> UpdateGroup([FromBody] RenameGroupCommand command)
        {
            var result = await Mediator.Send(command);

            return Ok(result);
        }
         
        [HttpDelete("file-group")]
        public async Task<IActionResult> DeleteFileOrGroup([FromQuery] DeleteFileOrGroupCommand command)
        {
            var result = await Mediator.Send(command);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddTemplate([FromBody] AddTemplateCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
        // WIDEMAPP_PLACEHOLDER
    }
}
