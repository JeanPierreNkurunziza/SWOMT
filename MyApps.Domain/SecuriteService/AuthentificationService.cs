using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace MyApps.Domain.SecuriteService
{
    /// <summary>
    /// Méthode de protection des données sensibles des utilisateurs 
    /// </summary>
   public class AuthentificationService
    {
       /// <summary>
       /// Controle des exceptions 
       /// </summary>
        public class InvalidHashException : Exception
        {
            public InvalidHashException() { }
            public InvalidHashException(string message)
                : base(message) { }
            public InvalidHashException(string message, Exception inner)
                : base(message, inner) { }
        }

        public class CannotPerformOperationException : Exception
        {
            public CannotPerformOperationException() { }
            public CannotPerformOperationException(string message)
                : base(message) { }
            public CannotPerformOperationException(string message, Exception inner)
                : base(message, inner) { }
        }

        /// <summary>
        /// Le PBKDF2 (abréviation de Password-Based Key Derivation Function 2
        /// Le PBKDF2 utilisé dans cette méthode applique une fonction choisie par l'utilisateur (fonction de hachage, de chiffrement ou un HMAC) 
        /// à un mot de passe ou une phrase secrète avec un sel et répète cette opération plusieurs fois afin de générer une clé, 
        /// qui peut être ensuite utilisée pour chiffrer un quelconque contenu.
        //Cette génération rajoute du temps de calcul qui complique le cassage du mot de passe, notamment par force brute
        /// </summary>
        public class PasswordSecurity 
        {
          
            // Les élément suivants peuvent être ajustés pour la complexité de hashing sans autant de changer le hashing méthode
            public const int SALT_BYTES = 24;
            public const int HASH_BYTES = 18;
            public const int PBKDF2_ITERATIONS = 64000;

            // Les élément suivants sont définies pour les placer dans l'ordre choisie et ne peut pas être changé
            public const int HASH_SECTIONS = 5;
            public const int HASH_ALGORITHM_INDEX = 0;
            public const int ITERATION_INDEX = 1;
            public const int HASH_SIZE_INDEX = 2;
            public const int SALT_INDEX = 3;
            public const int PBKDF2_INDEX = 4;

            /// <summary>
            /// Méthode pour hash un mot de passe 
            /// </summary>
            /// <param name="password"></param>
            /// <returns>Mot de passe hashé </returns>
            public static string CreateHash(string password)
            {
                // Générer le salt au hasard
                //le sel signifie que vous pouvez utiliser la même phrase de  mot passe pour générer plusieurs clés différentes
                // Les hachages de mot de passe ont besoin d'un sel afin que des mots de passe identiques ne correspondent pas au même hachage
                byte[] salt = new byte[SALT_BYTES];
                try
                {
                    
                    using (RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider())
                    {
                        csprng.GetBytes(salt);
                    }
                }
                catch (CryptographicException ex)
                {
                    throw new CannotPerformOperationException(
                        "Générateur de nombres aléatoires non disponible .",
                        ex
                    );
                }
                catch (ArgumentNullException ex)
                {
                    throw new CannotPerformOperationException(
                        "Argument non valide au générateur de nombres aléatoires.",
                        ex
                    );
                }

                // PBKDF2 prend plusieurs paramètres d'entrée et produit la clé dérivée en sortie
                byte[] hash = PBKDF2(password, salt, PBKDF2_ITERATIONS, HASH_BYTES);

                // format: algorithm:iterations:hashSize:salt:hash
                String parts = "sha1:" +
                    PBKDF2_ITERATIONS + //PBKDF2 permet de configurer le nombre d'itérations et ainsi de configurer le temps nécessaire pour dériver la clé 
                    ":" +
                    hash.Length +
                    ":" +
                    Convert.ToBase64String(salt) +
                    ":" +
                    Convert.ToBase64String(hash);
                return parts;
            }
            /// <summary>
            /// la vérification de mot de passe hashé selon le mode choisie
            /// </summary>
            /// <param name="password"></param>
            /// <param name="goodHash"></param>
            /// <returns></returns>
            public static bool VerifyPassword(string password, string goodHash)
            {
                char[] delimiter = { ':' };
                string[] split = goodHash.Split(delimiter);

                if (split.Length != HASH_SECTIONS)
                {
                    throw new InvalidHashException(
                        "Des champs sont manquants dans le hachage du mot de passe."
                    );
                }

                
                if (split[HASH_ALGORITHM_INDEX] != "sha1")
                {
                    throw new CannotPerformOperationException(
                        "Type de hachage non pris en charge."
                    );
                }

                int iterations = 0;
                try
                {
                    iterations = Int32.Parse(split[ITERATION_INDEX]);
                }
                catch (ArgumentNullException ex)
                {
                    throw new CannotPerformOperationException(
                        "Argument non valide",
                        ex
                    );
                }
                catch (FormatException ex)
                {
                    throw new InvalidHashException(
                        "Impossible d'analyser le nombre d'itérations comme un entier .",
                        ex
                    );
                }
                catch (OverflowException ex)
                {
                    throw new InvalidHashException(
                        "Le nombre d'itérations est trop grand pour être représenté.",
                        ex
                    );
                }

                if (iterations < 1)
                {
                    throw new InvalidHashException(
                        "Invalid number of iterations. Must be >= 1."
                    );
                }

                byte[] salt = null;
                try
                {
                    salt = Convert.FromBase64String(split[SALT_INDEX]);
                }
                catch (ArgumentNullException ex)
                {
                    throw new CannotPerformOperationException(
                        "Argument non valide donné à Convert.FromBase64String ",
                        ex
                    );
                }
                catch (FormatException ex)
                {
                    throw new InvalidHashException(
                        "Échec du décodage du sel en base64.",
                        ex
                    );
                }

                byte[] hash = null;
                try
                {
                    hash = Convert.FromBase64String(split[PBKDF2_INDEX]);
                }
                catch (ArgumentNullException ex)
                {
                    throw new CannotPerformOperationException(
                        "Argument non valide donné à Convert.FromBase64String",
                        ex
                    );
                }
                catch (FormatException ex)
                {
                    throw new InvalidHashException(
                        "Le décodage en base64 de la sortie pbkdf2 a échoué.",
                        ex
                    );
                }

                int storedHashSize = 0;
                try
                {
                    storedHashSize = Int32.Parse(split[HASH_SIZE_INDEX]);
                }
                catch (ArgumentNullException ex)
                {
                    throw new CannotPerformOperationException(
                        "Argument non valide dans Int32.Parse",
                        ex
                    );
                }
                catch (FormatException ex)
                {
                    throw new InvalidHashException(
                        "Impossible d'analyser la taille de hachage en tant qu'entier .",
                        ex
                    );
                }
                catch (OverflowException ex)
                {
                    throw new InvalidHashException(
                        "La taille de hachage est trop grande pour être représentée.",
                        ex
                    );
                }

                if (storedHashSize != hash.Length)
                {
                    throw new InvalidHashException(
                        "Hash length doesn't match stored hash length."
                    );
                }

                byte[] testHash = PBKDF2(password, salt, iterations, hash.Length);
                return SlowEquals(hash, testHash);
            }

            /// <summary>
            /// vérifier si c'est le meme mot de passe hashé 
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <returns></returns>
            private static bool SlowEquals(byte[] a, byte[] b)
            {
                uint diff = (uint)a.Length ^ (uint)b.Length;
                for (int i = 0; i < a.Length && i < b.Length; i++)
                {
                    diff |= (uint)(a[i] ^ b[i]);
                }
                return diff == 0;
            }

            /// <summary>
            /// Rfc2898DeriveBytes est une implémentation de PBKDF2.
            /// Ce qu'il fait, c'est hacher à plusieurs reprises le mot de passe de l'utilisateur avec le sel 
            /// </summary>
            /// <param name="password"></param>
            /// <param name="salt"></param>
            /// <param name="iterations"></param>
            /// <param name="outputBytes"></param>
            /// <returns></returns>
            private static byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
            {
                using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt))
                {
                    pbkdf2.IterationCount = iterations;
                    return pbkdf2.GetBytes(outputBytes);
                }
            }
            /// <summary>
            /// méthode pour vérifier si le mot de passe en parametre correspond à son utilisateur 
            /// </summary>
            /// <param name="motDePasse"></param>
            /// <param name="nomUser"></param>
            /// <returns> le nom d'utilisateur correspond au mot de passe en parametre </returns>
            public static string VerifyCrypto(string motDePasse, string nomUser)
            {
                string nom=null;
             
                using (MyApps.Infrastructure.DB.TrainingDBEntities db = new MyApps.Infrastructure.DB.TrainingDBEntities())
                {

                    foreach (var user in db.Utilisateurs)
                    {

                        if (user.UserName == nomUser)
                        {
                            var temp = user.MotDePasse;
                            // vérifier le mot de passé hashé 
                            bool estBon = PasswordSecurity.VerifyPassword(motDePasse, temp);
                            if (estBon == true)
                            {
                                 nom = nomUser;
                            }
                            else
                            {
                                nom = null;
                            }
                        
                        }
                       
                    }
                
                }
                return nom;
            }

        }
       
    }
}
