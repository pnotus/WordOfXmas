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

// approved_permutations_1 =   638 078 kombinationer
// approved_permutations_2 =   638 078 kombinationer
// approved_permutations_3 =   334 552 kombinationer
// approved_permutations_4 = 2 424 953 kombinationer
// approved_permutations_5 =   214 295 kombinationer
// approved_permutations_6 = 2 250 381 kombinationer

var file01 = new FileInfo(Path.Combine(dataDir, "combined_approved_permutations_0_1.txt"));
var file02 = new FileInfo(Path.Combine(dataDir, "combined_approved_permutations_0_2.txt"));
var file03 = new FileInfo(Path.Combine(dataDir, "combined_approved_permutations_0_3.txt"));
var file04a = new FileInfo(Path.Combine(dataDir, "combined_approved_permutations_0_4a.txt"));
var file04b = new FileInfo(Path.Combine(dataDir, "combined_approved_permutations_0_4b.txt"));
var file04c = new FileInfo(Path.Combine(dataDir, "combined_approved_permutations_0_4c.txt"));
var file04d = new FileInfo(Path.Combine(dataDir, "combined_approved_permutations_0_4d.txt"));
var file04e = new FileInfo(Path.Combine(dataDir, "combined_approved_permutations_0_4e.txt"));
var file05 = new FileInfo(Path.Combine(dataDir, "combined_approved_permutations_0_5.txt"));
var file06a = new FileInfo(Path.Combine(dataDir, "combined_approved_permutations_0_6a.txt"));
var file06b = new FileInfo(Path.Combine(dataDir, "combined_approved_permutations_0_6b.txt"));
var file06c = new FileInfo(Path.Combine(dataDir, "combined_approved_permutations_0_6c.txt"));
var file06d = new FileInfo(Path.Combine(dataDir, "combined_approved_permutations_0_6d.txt"));
var file06e = new FileInfo(Path.Combine(dataDir, "combined_approved_permutations_0_6e.txt"));

// var approver = new WordApprover(approved_permutations_0, approved_permutations_5, file05, new[] { 0, 5 });
// await approver.WriteApprovedAsync(CancellationToken.None);
var approvers = new []
{
    new WordApprover(approved_permutations_0, approved_permutations_1, file01, new[] { 0, 1 }),
    new WordApprover(approved_permutations_0, approved_permutations_2, file02, new[] { 0, 2 }),
    new WordApprover(approved_permutations_0, approved_permutations_3, file03, new[] { 0, 3 }),
    new WordApprover(approved_permutations_0, approved_permutations_4[0..484990], file04a, new[] { 0, 4 }),
    new WordApprover(approved_permutations_0, approved_permutations_4[484990..969980], file04b, new[] { 0, 4 }),
    new WordApprover(approved_permutations_0, approved_permutations_4[969980..1454970], file04c, new[] { 0, 4 }),
    new WordApprover(approved_permutations_0, approved_permutations_4[1454970..1939960], file04d, new[] { 0, 4 }),
    new WordApprover(approved_permutations_0, approved_permutations_4[1939960..], file04e, new[] { 0, 4 }),
    new WordApprover(approved_permutations_0, approved_permutations_5, file05, new[] { 0, 5 }),
    new WordApprover(approved_permutations_0, approved_permutations_6[0..450076], file06a, new[] { 0, 6 }),
    new WordApprover(approved_permutations_0, approved_permutations_6[450076..900152], file06b, new[] { 0, 6 }),
    new WordApprover(approved_permutations_0, approved_permutations_6[900152..1350228], file06c, new[] { 0, 6 }),
    new WordApprover(approved_permutations_0, approved_permutations_6[1350228..1800304], file06d, new[] { 0, 6 }),
    new WordApprover(approved_permutations_0, approved_permutations_6[1800304..], file06e, new[] { 0, 6 }),
};

ParallelOptions parallelOptions = new()
{
    MaxDegreeOfParallelism = 16
};

await Parallel.ForEachAsync(approvers, parallelOptions, async (approver, token) => 
{
    await approver.WriteApprovedAsync(token);
});