import sys
from pathlib import Path


base_path = Path(__file__).parent


def createEntity(entity_path: str, name: str):
    api_path = base_path.joinpath("Modules", name)
    
    entity_path = api_path.joinpath("entity")
    entity_path.mkdir(parents=True, exist_ok=True)
    content_file = f"""using invetario_api.Modules.products.entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace invetario_api.Modules.{name}.entity
{{
    [Table("{name.capitalize()}s")]
    public class {name.capitalize()}
   {{
    }}
}}
"""
    file_path = Path(entity_path).joinpath(f"{name.capitalize()}.cs")
    with open(file_path, "w") as file:
        file.write(content_file)

def createDto(entity_path: str, name: str):
    dto_path = Path(entity_path).joinpath("dto")
    dto_path.mkdir(parents=True, exist_ok=True)
    create_dto = f"""using System.ComponentModel.DataAnnotations;

namespace invetario_api.Modules.{name}.dto
{{
    public class {name.capitalize()}Dto
  { {
    }}
}}
"""

    update_dto = f"""using System.ComponentModel.DataAnnotations;

namespace invetario_api.Modules.{name}.dto
{{
    public class Update{name.capitalize()}Dto : {name.capitalize()}Dto
   { {
    }}
}}
"""
    file_path = Path(dto_path).joinpath(f"{name.capitalize()}Dto.cs")
    with open(file_path, "w") as file:
        file.write(create_dto)
    
    file_path = Path(dto_path).joinpath(f"Update{name.capitalize()}Dto.cs")
    with open(file_path, "w") as file:
        file.write(update_dto)


def createInterface(entity_path: str, name: str):
    interface = f"""using invetario_api.Modules.{name}.dto;
using invetario_api.Modules.{name}.entity;
using invetario_api.utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace invetario_api.Modules.{name}
{{
    public interface I{name.capitalize()}Service
    {{
        Task<List<{name.capitalize()}>> get{name.capitalize()}s();

        Task<{name.capitalize()}?> get{name.capitalize()}ById(int {name}Id);
        
        Task<{name.capitalize()}> create{name.capitalize()}({name.capitalize()}Dto data);

        Task<{name.capitalize()}?> update{name.capitalize()}(int {name}Id, Update{name.capitalize()}Dto data);

        Task<{name.capitalize()}?> delete{name.capitalize()}(int {name}Id);
    }}
}}
"""
    interface_path = Path(entity_path).joinpath(f"I{name.capitalize()}Service.cs")
    with open(interface_path, "w") as file:
        file.write(interface)


def createService(entity_path: str, name: str):
    service = f"""using invetario_api.database;
using invetario_api.Modules.{name}.dto;
using invetario_api.Modules.{name}.entity;
using invetario_api.utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace invetario_api.Modules.{name}
{{
    public class {name.capitalize()}Service : I{name.capitalize()}Service
    {{
        private Database _db;

        public {name.capitalize()}Service(Database db) {{ 
            _db = db;
        }}

        public async Task<List<{name.capitalize()}>> get{name.capitalize()}s()
        {{
            throw new NotImplementedException();
        }}

        public async Task<{name.capitalize()}> create{name.capitalize()}({name.capitalize()}Dto data)
        {{   
            throw new NotImplementedException();
        }}

        public async Task<{name.capitalize()}?> delete{name.capitalize()}(int {name}Id)
        {{
            throw new NotImplementedException();
        }}

        public async Task<{name.capitalize()}?> get{name.capitalize()}ById(int {name}Id)
        {{
            throw new NotImplementedException();
        }}

        public async Task<{name.capitalize()}?> update{name.capitalize()}(int {name}Id, Update{name.capitalize()}Dto data)
        {{
            throw new NotImplementedException();
        }}
    }}
}}
"""
    service_path = Path(entity_path).joinpath(f"{name.capitalize()}Service.cs")
    with open(service_path, "w") as file:
        file.write(service)

def createController(entity_path: str, name: str):
    controller = f"""using invetario_api.Modules.{name}.dto;
using invetario_api.Modules.{name}.entity;
using invetario_api.utils;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace invetario_api.Modules.{name}
{{

    [ApiController]
    [Route("api/[controller]")]
    public class {name.capitalize()}Controller : ControllerBase
    {{
        private I{name.capitalize()}Service _{name}Service;

        public {name.capitalize()}Controller(I{name.capitalize()}Service {name}Service) {{
            _{name}Service = {name}Service;
        }}

        [HttpGet]
        public async Task<IActionResult> FindAll() 
        {{
            var result = await _{name}Service.get{name.capitalize()}s();
            return Ok(result);
        }}
        
        [HttpGet("{{{name}Id:int}}")]
        public async Task<IActionResult> FindById(int {name}Id) 
        {{
            var result = await _{name}Service.get{name.capitalize()}ById({name}Id);
            return Ok(result);
        }}

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] {name.capitalize()}Dto data)
        {{
            var result = await _{name}Service.create{name.capitalize()}(data);
            return Ok(result);
        }}

        [HttpPut("{{{name}Id:int}}")]
        public async Task<IActionResult> update(int {name}Id, [FromBody] Update{name.capitalize()}Dto data)
        {{            
            var result = await _{name}Service.update{name.capitalize()}({name}Id, data);
            return Ok(result);
        }}


        [HttpDelete("{{{name}Id:int}}")]
        public async Task<IActionResult> delete(int {name}Id)
        {{
            var result = await _{name}Service.delete{name.capitalize()}({name}Id);
            return Ok(result);
        }}
    }}
}}
"""
    controller_path = Path(entity_path).joinpath(f"{name.capitalize()}Controller.cs")
    with open(controller_path, "w") as file:
        file.write(controller)


def updateDatabase(name: str):
    find_path = base_path / "database" / "Database.cs"

    with open(find_path, "r", encoding="utf-8") as file:
        content = file.read()

    namespace_index = content.find("namespace invetario_api.database")
    if namespace_index == -1:
        raise Exception("Namespace no encontrado")

    constructor_line = "public Database(DbContextOptions<Database> options): base(options) { }"
    constructor_index = content.find(constructor_line)
    if constructor_index == -1:
        raise Exception("Constructor no encontrado")

    entity_name = name[0].upper() + name[1:]
    dbset_line = f"public DbSet<{entity_name}> {name}s {{ get; set; }}\n\n"

    content = (
        content[:namespace_index]
        + f"using invetario_api.Modules.{name}.entity;\n"
        + content[namespace_index:]
    )

    constructor_index = content.find(constructor_line)
    content = (
        content[:constructor_index]
        + dbset_line
        + content[constructor_index:]
    )

    with open(find_path, "w", encoding="utf-8") as file:
        file.write(content)
        
def add_service_to_program(name: str):
    program_path = base_path / "Program.cs"

    with open(program_path, "r", encoding="utf-8") as file:
        content = file.read()

  
    builder_marker = "var builder = WebApplication.CreateBuilder(args);"
    builder_index = content.find(builder_marker)
    if builder_index == -1:
        raise Exception("No se encontró 'var builder = WebApplication.CreateBuilder(args);'")

    using_line = f"using invetario_api.Modules.{name};\n"

    if using_line not in content:
        content = content[:builder_index] + using_line + content[builder_index:]


    marker = "builder.Services.Configure<ApiBehaviorOptions>(opt =>"
    marker_index = content.find(marker)

    if marker_index == -1:
        raise Exception("No se encontró el bloque Configure<ApiBehaviorOptions>")

    service_line = f"builder.Services.AddScoped<I{name.capitalize()}Service, {name.capitalize()}Service>();\n"

    if service_line not in content:
        content = content[:marker_index] + service_line + "\n" + content[marker_index:]

    with open(program_path, "w", encoding="utf-8") as file:
        file.write(content)




def main():
    
    name = sys.argv[1]
    
    if not name:
        print("Error: Name is required")
        return
    
    print(f"Create {name} api")
    
    
    api_path = base_path.joinpath("Modules", name)
    api_path.mkdir(parents=True, exist_ok=True)
  
    createEntity(entity_path=str(api_path), name=name)
    
    createDto(entity_path=str(api_path), name=name)
    
    createInterface(entity_path=str(api_path), name=name)
    createService(entity_path=str(api_path), name=name)
    createController(entity_path=str(api_path), name=name)
    updateDatabase(name=name)
    add_service_to_program(name=name)
    
    
    
if __name__ == "__main__":
    main()