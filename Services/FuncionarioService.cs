using SistemaTurnos.Web.Data.Entities;
using SistemaTurnos.Web.Repositories;
using SistemaTurnos.Web.Repositories.Interfaces;
using SistemaTurnos.Web.Services.Interfaces;

namespace SistemaTurnos.Web.Services;

public class FuncionarioService: IFuncionarioService
{
    private readonly IFuncionarioRepository _funcionarioRepository;
    private readonly BaseCrudRepository<Funcionario>  _baseRepository;

    public FuncionarioService(IFuncionarioRepository funcionarioRepository,BaseCrudRepository<Funcionario> baseRepository)
    {
        _funcionarioRepository = funcionarioRepository;
        _baseRepository = baseRepository;
    }
    
    
    
    public async Task<Funcionario?> AutenticarFuncionarioAsync(string documento)
    {
        // Authenticamos el funcionario por numero de documento
        var funcionario = await _funcionarioRepository.ObtenerPorDocumentoAsync(documento);
        
        // Debe existir y estar activo para poder ingresar:
        if (funcionario == null || !funcionario.Estado)
        {
            return null;
        } 
        return funcionario;
    }

    public async Task<List<Funcionario>> ObtenerFuncionariosActivosAsync()
    {
        // Retornamos la lista de los funcionarios activos:
        return await _funcionarioRepository.ObtenerActivosAsync();
    }
}