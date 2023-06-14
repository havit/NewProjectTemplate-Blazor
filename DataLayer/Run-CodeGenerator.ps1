#&dotnet tool update havit.data.entityframeworkcore.codegenerator.tool --ignore-failed-sources
&dotnet tool restore
&dotnet efcodegenerator
