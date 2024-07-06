using concepts_poc.services.mock;
using System.Text.Json;


var serializerOpts = new JsonSerializerOptions
{
     PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
     WriteIndented = true,
};

var serialize = (object data) => JsonSerializer.Serialize(data, serializerOpts);

var context = DummyConceptDataMocker.BuildPayrollContext();
var conceptsFromDB = DummyConceptDataMocker.GenerateDummyData();
var conceptsMapperFactory = DummyConceptDataMocker.BuildConceptMapperFactory();

Console.WriteLine($"context: {serialize(context)}");
Console.WriteLine($"concepts from db: {serialize(conceptsFromDB)}");

Console.WriteLine("\nmapping concetps...");
var mappedConcepts = DummyConceptDataMocker.BuildDepedencyTree(conceptsFromDB);
Console.WriteLine($"mapped concepts to business: {mappedConcepts} -> {serialize(mappedConcepts)}");

Console.WriteLine("Calculating concepts");

foreach (var concept in mappedConcepts)
{
    Console.WriteLine($"calculating concept {concept.Code}");
    concept.Value = concept.Calculate(context);

    Console.WriteLine(serialize(concept));

}

Console.WriteLine("\ncompleted!");