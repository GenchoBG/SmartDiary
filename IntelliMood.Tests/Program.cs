using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SVDPP
{
    public class SVDPP
    {
        public int Factors = 5;         // The number of latent factors
        public double TS = 0.025;       // The training speed 
        public double L1 = 0.0005;      // Regularization coefficient lambda1
        public double L2 = 0.0025;      // Regularization coefficient lambda2
        public double EPS = 0.00001;    // Error precision accuracy coefficient
        public double Threshold = 0.01; // Threshold coefficient
        public List<double> BS_User = new List<double>(); // Declaring the vector of users baseline predictors
        public List<double> BS_Item = new List<double>(); // Declaring the vector of items baseline predictors
        public List<List<double>> MF_User = new List<List<double>>(); // Declaring the matrix of user's latent factors
        public List<List<double>> MF_Item = new List<List<double>>(); // Declaring the matrix of item's latent factors
        public List<List<double>> MatrixUI = new List<List<double>>(); // Declaring the matrix of ratings
        public readonly Random rand = new Random();

        public void Initialize()
        {
            // Constructing the matrix of user's latent factors by iteratively
            // appending the rows being constructed to the list of rows MF_UserRow
            for (var User = 0; User < this.MatrixUI.Count(); User++)
            {
                // Declare a list of items MF_UserRow rated by the current user
                var MF_UserRow = new List<double>();
                // Add the set of elements equal to 0 to the list of items MF_UserRow.
                // The number of elements being added is stored in Factors variable
                MF_UserRow.AddRange(Enumerable.Repeat(0.00, this.Factors));
                // Append the current row MF_UserRow to the matrix of factors MF_User
                this.MF_User.Insert(User, MF_UserRow);
            }

            // Constructing the matrix of item's latent factors by iteratively
            // appending the rows being constructed to the list of rows MF_ItemRow
            for (var Item = 0; Item < this.MatrixUI.ElementAt(0).Count(); Item++)
            {
                // Declare a list of items MF_ItemRow rated by the current item
                var MF_ItemRow = new List<double>();
                // Add the set of elements equal to 0 to the list of items MF_ItemRow
                // The number of elements being added is stored in Factors variable
                MF_ItemRow.AddRange(Enumerable.Repeat(0.00, this.Factors));
                // Append the current row MF_ItemRow to the matrix of factors MF_Item
                this.MF_Item.Insert(Item, MF_ItemRow);
            }

            // Intializing the first elements of the matrices of user's 
            // and item's factors with values 0.1 and 0.05
            this.MF_User[0][0] = 0.1;
            this.MF_Item[0][0] = this.MF_User[0][0] / 2;

            // Construct the vector of users baseline predictors by 
            // appending the set of elements equal to 0.The number of elements being 
            // appended is equal to the actual number of rows in the matrix of ratings
            this.BS_User.AddRange(Enumerable.Repeat(0.00, this.MatrixUI.Count()));
            // Construct the vector of items baseline predictors by appending
            // the set of elements equal to 0. The number of elements appended 
            // is equal to the actual number of rows in the matrix of ratings
            this.BS_Item.AddRange(Enumerable.Repeat(0.00, this.MatrixUI.ElementAt(0).Count()));
        }

        public double GetProduct(List<double> VF_User, List<double> VF_Item)
        {
            // Initialize the variable that is used to 
            // store the inner product of two factorization vectors
            var Product = 0.00;
            // Iterating through the two factorization vectors
            for (var Index = 0; Index < this.Factors; Index++)
                // Compute the value of product of the two components 
                // of those vectors having the same value of index and 
                // add this value to the value of the variable Product
                Product += VF_User[Index] * VF_Item[Index];

            return Product;
        }

        public double GetAverageRating(List<List<double>> Matrix)
        {
            // Initialize the variables Sum and Count to store the values of
            // sum of existing ratings in matrix of ratings and the count of
            // existing ratings respectively
            double Sum = 0; var Count = 0;
            // Iterating through the matrix of ratings
            for (var User = 0; User < Matrix.Count(); User++)
                for (var Item = 0; Item < Matrix[User].Count(); Item++)
                    // For each rating performing a check if the current rating is unknown
                    if (Matrix[User][Item] > 0)
                    {
                        // If not, add the value of the current rating to the value of variable Sum
                        Sum = Sum + Matrix[User][Item];
                        // Increment the loop counter variable of existing ratings by 1
                        Count = Count + 1;
                    }

            // Compute and return the value of average 
            // rating for the entire domain of existing ratings
            return Sum / Count;
        }

        public void LoadItemsFromMatrix(List<List<double>> matrix, List<List<double>> Matrix)
        {
            foreach (var vector in matrix)
            {
                var row = vector.ToList();
                Matrix.Add(row);
            }
        }

        public void LoadItemsFromFile(string Filename, List<List<double>> Matrix)
        {
            // Intializing the file stream object and open the file
            using (var fsFile = new System.IO.FileStream(Filename, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read))
            {
                // Initializing the stream reader object
                using (var fsStream = new System.IO.StreamReader(fsFile, System.Text.Encoding.UTF8, true, 128))
                {
                    var textBuf = "\0";
                    // Retrieving each line from the file until we reach the end-of-file
                    while ((textBuf = fsStream.ReadLine()) != null)
                    {
                        var Row = new List<double>();
                        if (!String.IsNullOrEmpty(textBuf))
                        {
                            var sPattern = " ";
                            // Iterating through the array of tokens and append each token to the array Row
                            foreach (var rating in Regex.Split(textBuf, sPattern))
                                Row.Add(double.Parse(rating));
                        }

                        // Append the current row to the matrix of ratings
                        Matrix.Add(Row);
                    }
                }
            }
        }

        public void Learn()
        {
            // Initializing the iterations loop counter variable
            var Iterations = 0;
            // Initializing the RMSE and RMSE_New variables to store
            // current and previous values of RMSE
            double RMSE = 0.00, RMSE_New = 1.00;
            // Computing the average rating for the entire domain of rated items
            var AvgRating = this.GetAverageRating(this.MatrixUI);
            // Iterating the process of the ratings prediction model update until
            // the value of difference between the current and previous value of RMSE
            // is greater than the value of error precision accuracy EPS (e.g. the learning
            // process has converged).
            while (Math.Abs(RMSE - RMSE_New) > this.EPS)
            {
                // Assign the previously obtained value of RMSE to the RMSE variable
                // Assign the variable RMSE_New equal to 0
                RMSE = RMSE_New; RMSE_New = 0;
                // Iterate through the matrix of ratings and for each existing rating compute
                // the error value and perform the stochastic gradient descent to update 
                // the main parameters of the ratings prediction model for the current user and item
                for (var User = 0; User < this.MatrixUI.Count(); User++)
                {
                    for (var Item = 0; Item < this.MatrixUI.ElementAt(0).Count(); Item++)
                        // Perform a check if the current rating in the matrix of ratings is unknown.
                        // If not, perform the following steps to adjust the values of baseline
                        // predictors and factorization vectors
                        if (this.MatrixUI[User].ElementAt(Item) > 0)
                        {
                            // Compute the value of estimated rating using formula (2)
                            var Rating = AvgRating + this.BS_User[User] + this.BS_Item[Item] + this.GetProduct(this.MF_User[User], this.MF_Item[Item]);

                            // Compute the error value as the difference between the existing and estimated ratings
                            var Error = this.MatrixUI[User].ElementAt(Item) - Rating;

                            // Output the current rating given by the current user to the current item
                            //Console.Write("{0:0.00}|{1:0.00} ", MatrixUI[User][Item], Rating);

                            // Add the value of error square to the current value of RMSE
                            RMSE_New = RMSE_New + Math.Pow(Error, 2);

                            // Update the value of average rating for the entire domain of ratings
                            // by performing stochastic gradient descent using formulas (7.1-5)
                            AvgRating = AvgRating + this.TS * (Error - this.L1 * AvgRating);
                            // Update the value of baseline predictor of the current user
                            // by performing stochastic gradient descent using formulas (7.1-5)
                            this.BS_User[User] = this.BS_User[User] + this.TS * (Error - this.L1 * this.BS_User[User]);
                            // Update the value of baseline predictor of the current item 
                            // by performing stochastic gradient descent using formulas (7.1-5)
                            this.BS_Item[Item] = this.BS_Item[Item] + this.TS * (Error - this.L1 * this.BS_Item[Item]);

                            // Update each component of the factorization vector for the current user and item
                            for (var Factor = 0; Factor < this.Factors; Factor++)
                            {
                                // Adjust the value of the current component of the user's factorization vector 
                                // by performing stochastic gradient descent using formulas (7.1-5)
                                this.MF_User[User][Factor] += this.TS * (Error * this.MF_Item[Item][Factor] + this.L2 * this.MF_User[User][Factor]);
                                // Adjust the value of the current component of the item's factorization vector 
                                // by performing stochastic gradient descent using formulas (7.1-5)
                                this.MF_Item[Item][Factor] += this.TS * (Error * this.MF_User[User][Factor] + this.L2 * this.MF_Item[Item][Factor]);
                            }
                        }

                    // Output the value of unknown rating in the matrix of ratings
                    //else Console.Write("{0:0.00}|0.00 ", MatrixUI[User][Item]);

                    //Console.WriteLine("\n");
                }

                // Compute the current value of RMSE (root means square error)
                RMSE_New = Math.Sqrt(RMSE_New / (this.MatrixUI.Count() * this.MatrixUI.ElementAt(0).Count()));

                //Console.WriteLine("Iteration: {0}\t RMSE={1}\n\n", Iterations, RMSE_New);

                // Performing a check if the difference between the values 
                // of current and previous values of RMSE exceeds the given threshold
                if (RMSE_New > RMSE - this.Threshold)
                {
                    // If so, reduce the values of training speed and threshold 
                    // by multiplying each value by the value of specific coefficients
                    this.TS *= 0.66;
                    this.Threshold *= 0.5;
                }

                Iterations++; // Increment the iterations loop counter variable
            }
        }

        public void Predict()
        {
            // Computing the average rating for the entire domain of rated items
            var AvgRating = this.GetAverageRating(this.MatrixUI);
            //Console.WriteLine("We've predicted the following ratings:\n");
            // Iterating through the MatrixUI matrix of ratings
            for (var User = 0; User < this.MatrixUI.Count(); User++)
                for (var Item = 0; Item < this.MatrixUI.ElementAt(0).Count(); Item++)
                    // For each rating given to the current item by the current user 
                    // we're performing a check if the current item is unknown
                    if (this.MatrixUI[User].ElementAt(Item) == 0)
                    {
                        // If so, compute the rating for the current 
                        // unrated item used baseline estimate formula (2)
                        this.MatrixUI[User][Item] = AvgRating + this.BS_User[User] + this.BS_Item[Item] + this.GetProduct(this.MF_User[User], this.MF_Item[Item]);

                        // Output the original rating estimated for the current item 
                        // and the rounded value of the following rating
//                        Console.WriteLine("User {0} has rated Item {1} as {2:0.00}|{3:0.00}", User, Item, this.MatrixUI[User][Item], Math.Round(this.MatrixUI[User][Item]));
                    }

//            Console.WriteLine();
        }
    }

    public class Predictor
    {
        public List<List<double>> GetPopulatedEmptySpots(List<List<double>> data)
        {
            var predictor = new SVDPP();

            // Loading matrix of ratings from file
            predictor.LoadItemsFromMatrix(data, predictor.MatrixUI);

            predictor.Initialize(); // Initializing the ratings prediction model

            predictor.Learn();      // Training the ratings prediction model

            predictor.Predict();    // Predicting ratings for the unrated items
            
            return predictor.MatrixUI;
        }
    }

    class Program
    {


        static void Main(string[] args)
        {
            var test = new List<List<double>>()
            {
                new List<double>(){ 0, 3, 4, 5, 2 },
                new List<double>(){ 3, 5, 2, 2, 5 },
                new List<double>(){ 5, 3, 0, 4, 3 },
                new List<double>(){ 5, 5, 5, 0, 5 },
                new List<double>(){ 2, 3, 0, 2, 2 }
            };

            var predictor = new Predictor();

            var populated = predictor.GetPopulatedEmptySpots(test);

            foreach (var row in populated)
            {
                Console.WriteLine(string.Join(" ", row.Select(n => n.ToString("F2"))));
            }

        }
    }
}
