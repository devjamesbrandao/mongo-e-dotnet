using Microsoft.AspNetCore.Mvc;
using Mongo.Data.Repositories;
using Mongo.Models;
using Mongo.Models.InputModels;

namespace Mongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefasController : ControllerBase
    {
        private ITarefasRepository _tarefasRepository;

        public TarefasController(ITarefasRepository tarefasRepository)
        {
            _tarefasRepository = tarefasRepository;
        }

        /// <summary>
        /// Obter todas as tarefas cadastradas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var tarefas = _tarefasRepository.Buscar();

            return Ok(tarefas);
        }

        /// <summary>
        /// Obter tarefa por id
        /// </summary>
        /// <param name="id">Id da tarefa</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var tarefa = _tarefasRepository.Buscar(id);

            if (tarefa is null) return NotFound();

            return Ok(tarefa);
        }

        /// <summary>
        /// Cadastrar nova tarefa
        /// </summary>
        /// <param name="novaTarefa"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody] TarefaInputModel novaTarefa)
        {
            var tarefa = new Tarefa(novaTarefa.Nome, novaTarefa.Detalhes);

            _tarefasRepository.Adicionar(tarefa);

            return Created("", tarefa);
        }

        /// <summary>
        /// Atualizar tarefa
        /// </summary>
        /// <param name="id">Id da tarefa</param>
        /// <param name="tarefaAtualizada"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] TarefaInputModel tarefaAtualizada)
        {
            var tarefa = _tarefasRepository.Buscar(id);

            if (tarefa is null) return NotFound();

            tarefa.AtualizarTarefa(tarefaAtualizada.Nome, tarefaAtualizada.Detalhes, tarefaAtualizada.Concluido);

            _tarefasRepository.Atualizar(id, tarefa);

            return Ok(tarefa);
        }

        /// <summary>
        /// Excluir tarefa
        /// </summary>
        /// <param name="id">Id da tarefa</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var tarefa = _tarefasRepository.Buscar(id);

            if (tarefa is null) return NotFound();

            _tarefasRepository.Remover(id);

            return NoContent();
        }
    }
}
