var dataDir = @"C:\Users\pontu\repos\WordOfXmas.net\data\";

// new DataGrabber(dataDir, "approved_permutations_0_5_Part*.txt").CombineFiles("approved_permutations_0_5.txt");
// Console.WriteLine("Klar med att kombinera filer.");

// new [] { 0, 3, 5 }.ToList().ForEach(number => new PointCalculator(number).IsApproved("DELKLTAUA"));
// Environment.Exit(0);

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

var approved_permutations_0_5 = File.ReadAllLines(Path.Combine(dataDir, "approved_permutations_0_5.txt"));
Console.WriteLine($"approved_permutations_0_5={approved_permutations_0_5.Length} kombinationer");
// Environment.Exit(0);

// approved_permutations_0 =    30 408 kombinationer
// approved_permutations_1 =   638 078 kombinationer
// approved_permutations_2 =   638 078 kombinationer
// approved_permutations_3 =   334 552 kombinationer
// approved_permutations_4 = 2 424 953 kombinationer
// approved_permutations_5 =   214 295 kombinationer
// approved_permutations_6 = 2 250 381 kombinationer

FileInfo CreateFileInfo(string FileName)
{
    return new FileInfo(Path.Combine(dataDir, FileName));
}

// var approver = new WordApprover(approved_permutations_0, approved_permutations_5, file05, new[] { 0, 5 });
// await approver.WriteApprovedAsync(CancellationToken.None);
var i = 1;
var approvers = new []
{
    // new WordApprover(approved_permutations_0, approved_permutations_5[     0..13394], CreateFileInfo($"approved_permutations_0_5_part{i++:D2}.txt"), new [] { 0, 5 }),
    // new WordApprover(approved_permutations_0, approved_permutations_5[ 13394..26788], CreateFileInfo($"approved_permutations_0_5_part{i++:D2}.txt"), new [] { 0, 5 }),
    // new WordApprover(approved_permutations_0, approved_permutations_5[ 26788..40182], CreateFileInfo($"approved_permutations_0_5_part{i++:D2}.txt"), new [] { 0, 5 }),
    // new WordApprover(approved_permutations_0, approved_permutations_5[ 40182..53576], CreateFileInfo($"approved_permutations_0_5_part{i++:D2}.txt"), new [] { 0, 5 }),
    // new WordApprover(approved_permutations_0, approved_permutations_5[ 53576..66970], CreateFileInfo($"approved_permutations_0_5_part{i++:D2}.txt"), new [] { 0, 5 }),
    // new WordApprover(approved_permutations_0, approved_permutations_5[ 66970..80364], CreateFileInfo($"approved_permutations_0_5_part{i++:D2}.txt"), new [] { 0, 5 }),
    // new WordApprover(approved_permutations_0, approved_permutations_5[ 80364..93758], CreateFileInfo($"approved_permutations_0_5_part{i++:D2}.txt"), new [] { 0, 5 }),
    // new WordApprover(approved_permutations_0, approved_permutations_5[ 93758..107152], CreateFileInfo($"approved_permutations_0_5_part{i++:D2}.txt"), new [] { 0, 5 }),
    // new WordApprover(approved_permutations_0, approved_permutations_5[107152..120546], CreateFileInfo($"approved_permutations_0_5_part{i++:D2}.txt"), new [] { 0, 5 }),
    // new WordApprover(approved_permutations_0, approved_permutations_5[120546..133940], CreateFileInfo($"approved_permutations_0_5_part{i++:D2}.txt"), new [] { 0, 5 }),
    // new WordApprover(approved_permutations_0, approved_permutations_5[133940..147334], CreateFileInfo($"approved_permutations_0_5_part{i++:D2}.txt"), new [] { 0, 5 }),
    // new WordApprover(approved_permutations_0, approved_permutations_5[147334..160728], CreateFileInfo($"approved_permutations_0_5_part{i++:D2}.txt"), new [] { 0, 5 }),
    // new WordApprover(approved_permutations_0, approved_permutations_5[160728..174122], CreateFileInfo($"approved_permutations_0_5_part{i++:D2}.txt"), new [] { 0, 5 }),
    // new WordApprover(approved_permutations_0, approved_permutations_5[174122..187516], CreateFileInfo($"approved_permutations_0_5_part{i++:D2}.txt"), new [] { 0, 5 }),
    // new WordApprover(approved_permutations_0, approved_permutations_5[187516..200910], CreateFileInfo($"approved_permutations_0_5_part{i++:D2}.txt"), new [] { 0, 5 }),
    // new WordApprover(approved_permutations_0, approved_permutations_5[200910..], CreateFileInfo($"approved_permutations_0_5_part{i++:D2}.txt"), new [] { 0, 5 }),

    new WordApprover(approved_permutations_3, approved_permutations_0_5[      0..185436], CreateFileInfo($"approved_permutations_0_3_5_part{i++:D2}.txt"), new [] { 0, 3, 5 }),
    new WordApprover(approved_permutations_3, approved_permutations_0_5[ 185436..370872], CreateFileInfo($"approved_permutations_0_3_5_part{i++:D2}.txt"), new [] { 0, 3, 5 }),
    new WordApprover(approved_permutations_3, approved_permutations_0_5[ 370872..556308], CreateFileInfo($"approved_permutations_0_3_5_part{i++:D2}.txt"), new [] { 0, 3, 5 }),
    new WordApprover(approved_permutations_3, approved_permutations_0_5[ 556308..741744], CreateFileInfo($"approved_permutations_0_3_5_part{i++:D2}.txt"), new [] { 0, 3, 5 }),
    new WordApprover(approved_permutations_3, approved_permutations_0_5[ 741744..927180], CreateFileInfo($"approved_permutations_0_3_5_part{i++:D2}.txt"), new [] { 0, 3, 5 }),
    new WordApprover(approved_permutations_3, approved_permutations_0_5[ 927180..1112616], CreateFileInfo($"approved_permutations_0_3_5_part{i++:D2}.txt"), new [] { 0, 3, 5 }),
    new WordApprover(approved_permutations_3, approved_permutations_0_5[1112616..1298052], CreateFileInfo($"approved_permutations_0_3_5_part{i++:D2}.txt"), new [] { 0, 3, 5 }),
    new WordApprover(approved_permutations_3, approved_permutations_0_5[1298052..1483488], CreateFileInfo($"approved_permutations_0_3_5_part{i++:D2}.txt"), new [] { 0, 3, 5 }),
    new WordApprover(approved_permutations_3, approved_permutations_0_5[1483488..1668924], CreateFileInfo($"approved_permutations_0_3_5_part{i++:D2}.txt"), new [] { 0, 3, 5 }),
    new WordApprover(approved_permutations_3, approved_permutations_0_5[1668924..1854360], CreateFileInfo($"approved_permutations_0_3_5_part{i++:D2}.txt"), new [] { 0, 3, 5 }),
    new WordApprover(approved_permutations_3, approved_permutations_0_5[1854360..2039796], CreateFileInfo($"approved_permutations_0_3_5_part{i++:D2}.txt"), new [] { 0, 3, 5 }),
    new WordApprover(approved_permutations_3, approved_permutations_0_5[2039796..2225232], CreateFileInfo($"approved_permutations_0_3_5_part{i++:D2}.txt"), new [] { 0, 3, 5 }),
    new WordApprover(approved_permutations_3, approved_permutations_0_5[2225232..2410668], CreateFileInfo($"approved_permutations_0_3_5_part{i++:D2}.txt"), new [] { 0, 3, 5 }),
    new WordApprover(approved_permutations_3, approved_permutations_0_5[2410668..2596104], CreateFileInfo($"approved_permutations_0_3_5_part{i++:D2}.txt"), new [] { 0, 3, 5 }),
    new WordApprover(approved_permutations_3, approved_permutations_0_5[2596104..2781540], CreateFileInfo($"approved_permutations_0_3_5_part{i++:D2}.txt"), new [] { 0, 3, 5 }),
    new WordApprover(approved_permutations_3, approved_permutations_0_5[2781540..], CreateFileInfo($"approved_permutations_0_3_5_part{i++:D2}.txt"), new [] { 0, 3, 5 }),

};

ParallelOptions parallelOptions = new()
{
    MaxDegreeOfParallelism = 16
};

await Parallel.ForEachAsync(approvers, parallelOptions, async (approver, token) => 
{
    await approver.WriteApprovedAsync(token);
});