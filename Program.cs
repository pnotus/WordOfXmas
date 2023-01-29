// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var dataDir = @"C:\Users\pontu\repos\WordOfXmas.net\data\";
// new DataGrabber(dataDir, "combined_approved_permutation_1_2_part*.txt").CombineFiles("approved_permutations_1_2.txt");

var approved_permutations_3 = File.ReadAllLines(Path.Combine(dataDir, "approved_permutations_3.txt"));
Console.WriteLine(approved_permutations_3.Length);


var approved_permutations_1_2 = File.ReadAllLines(Path.Combine(dataDir, "approved_permutations_1_2.txt"));
Console.WriteLine(approved_permutations_1_2.Length);

var wc = new WordApprover(approved_permutations_3, approved_permutations_1_2, 16, dataDir, "approved_permutations_1_2_3.txt", new[] {1, 2, 3});
Console.WriteLine("Size=" + wc.Size);
wc.WriteApproved();