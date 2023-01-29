// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var dataDir = @"C:\Users\pontu\repos\WordOfXmas.net\data\";
// new DataGrabber(dataDir, "combined_approved_permutation_1_2_part*.txt").CombineFiles("approved_permutations_1_2.txt");

var approved_permutations_0 = File.ReadAllLines(Path.Combine(dataDir, "approved_permutations_0.txt"));
Console.WriteLine($"approved_permutations_0={approved_permutations_0.Length} kombinationer");

var approved_permutations_1 = File.ReadAllLines(Path.Combine(dataDir, "approved_permutations_1.txt"));
Console.WriteLine($"approved_permutations_1={approved_permutations_1.Length} kombinationer");

var approved_permutations_2 = File.ReadAllLines(Path.Combine(dataDir, "approved_permutations_2.txt"));
Console.WriteLine($"approved_permutations_2={approved_permutations_2.Length} kombinationer");

var approved_permutations_3 = File.ReadAllLines(Path.Combine(dataDir, "approved_permutations_3.txt"));
Console.WriteLine($"approved_permutations_3={approved_permutations_3.Length} kombinationer");

var approved_permutations_4 = File.ReadAllLines(Path.Combine(dataDir, "approved_permutations_4.txt"));
Console.WriteLine($"approved_permutations_4={approved_permutations_4.Length} kombinationer");

var approved_permutations_5 = File.ReadAllLines(Path.Combine(dataDir, "approved_permutations_5.txt"));
Console.WriteLine($"approved_permutations_5={approved_permutations_5.Length} kombinationer");

var approved_permutations_6 = File.ReadAllLines(Path.Combine(dataDir, "approved_permutations_6.txt"));
Console.WriteLine($"approved_permutations_6={approved_permutations_6.Length} kombinationer");

var file01 = new FileInfo(Path.Combine(dataDir, "combined_approved_permutations_0_1.txt"));
var file02 = new FileInfo(Path.Combine(dataDir, "combined_approved_permutations_0_2.txt"));
var file03 = new FileInfo(Path.Combine(dataDir, "combined_approved_permutations_0_3.txt"));
var file04 = new FileInfo(Path.Combine(dataDir, "combined_approved_permutations_0_4.txt"));
var file05 = new FileInfo(Path.Combine(dataDir, "combined_approved_permutations_0_5.txt"));
var file06 = new FileInfo(Path.Combine(dataDir, "combined_approved_permutations_0_6.txt"));

Parallel.ForEach(new []
{
    new WordApprover(approved_permutations_0, approved_permutations_1, file01, new[] { 0, 1 }),
    new WordApprover(approved_permutations_0, approved_permutations_2, file02, new[] { 0, 2 }),
    new WordApprover(approved_permutations_0, approved_permutations_3, file03, new[] { 0, 3 }),
    new WordApprover(approved_permutations_0, approved_permutations_4, file04, new[] { 0, 4 }),
    new WordApprover(approved_permutations_0, approved_permutations_4, file05, new[] { 0, 5 }),
    new WordApprover(approved_permutations_0, approved_permutations_4, file06, new[] { 0, 6 }),
}, approver => approver.WriteApproved());