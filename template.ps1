$name = "ProductManagement"

dotnet new sln -n $name

dotnet new classlib -n "$name.Application" -o "src/$name.Application" -f net5.0
dotnet new classlib -n "$name.Domain" -o "src/$name.Domain" -f net5.0
dotnet new classlib -n "$name.Infrastructure" -o "src/$name.Infrastructure" -f net5.0

dotnet new webapi -n "$name.Presentation" -o "src/$name.Presentation" -f net5.0

dotnet sln add "src/$name.Application"
dotnet sln add "src/$name.Domain"
dotnet sln add "src/$name.Infrastructure"
dotnet sln add "src/$name.Presentation"

dotnet add "src/$name.Application" reference "src/$name.Domain"
dotnet add "src/$name.Infrastructure" reference "src/$name.Domain"
dotnet add "src/$name.Application" reference "src/$name.Infrastructure"
dotnet add "src/$name.Presentation" reference "src/$name.Application"
dotnet add "src/$name.Presentation" reference "src/$name.Infrastructure"