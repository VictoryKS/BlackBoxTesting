using System;
using Xunit;
using IIG.PasswordHashingUtils;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void TestGetHash()
        {
            string pass = "passwrd";

            string result = PasswordHasher.GetHash(pass);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.NotEqual(pass, result);
        }

        [Fact]
        public void TestGetHash2()
        {
            string pass = "passwrd";
            string salt = "salt";

            string result = PasswordHasher.GetHash(pass, salt);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.NotEqual(pass, result);
        }

        [Fact]
        public void TestGetHash3()
        {
            string pass = "passwrd";
            uint adlerMod32 = 16;

            string result = PasswordHasher.GetHash(pass, null, adlerMod32);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.NotEqual(pass, result);
        }

        [Fact]
        public void TestGetHash4()
        {
            string pass = "passwrd";
            string salt = "salt";
            uint adlerMod32 = 16;

            string result = PasswordHasher.GetHash(pass, salt, adlerMod32);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.NotEqual(pass, result);
        }

        [Fact]
        public void TestGetHashSpecialSymbols()
        {
            string pass1 = "紅蓮華 𝅘𝅥𝅯𝅘𝅥𝅯";
            string pass2 = "Кириллица";
            string salt1 = "salt";
            string salt2 = "𝄞𝅘𝅥𝅯𝅗𝅥";
            uint adlerMod32 = 16;

            string result1 = PasswordHasher.GetHash(pass1, salt1, adlerMod32);

            Assert.NotNull(result1);
            Assert.NotEmpty(result1);
            Assert.NotEqual(pass1, result1);

            string result2 = PasswordHasher.GetHash(pass2, salt1, adlerMod32);

            Assert.NotNull(result2);
            Assert.NotEmpty(result2);
            Assert.NotEqual(pass2, result2);

            Assert.ThrowsAny<OverflowException>(() => PasswordHasher.GetHash(pass1, salt2, adlerMod32));
        }

        [Fact]
        public void TestInit()
        {
            string pass = "passwrd";
            string salt1 = "salt1";
            string salt2 = "salt2";
            uint adlerMod32 = 16;

            PasswordHasher.Init(salt1, adlerMod32);
            Assert.Equal(PasswordHasher.GetHash(pass), PasswordHasher.GetHash(pass, salt1, adlerMod32));

            Assert.NotEqual(PasswordHasher.GetHash(pass), PasswordHasher.GetHash(pass, salt2, adlerMod32));
        }

        [Fact]
        public void TestInitNull()
        {
            string pass = "passwrd";
            string salt1 = null;
            uint adlerMod32 = 16;

            PasswordHasher.Init(salt1, adlerMod32);
            Assert.Equal(PasswordHasher.GetHash(pass), PasswordHasher.GetHash(pass, salt1, adlerMod32));
        }

        [Fact]
        public void TestPasswordNull()
        {
            string pass = null;
            Assert.Throws<ArgumentNullException>(() => PasswordHasher.GetHash(pass));
        }

        [Fact]
        public void TestPasswordEmpty()
        {
            string pass = "";

            Assert.NotNull(PasswordHasher.GetHash(pass));
            Assert.NotEmpty(PasswordHasher.GetHash(pass));
        }
    }
}
