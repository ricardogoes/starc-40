using AutoMoq;
using Moq;
using STARC.Application;
using STARC.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using Xunit;

namespace STARC.Tests.Password
{
    [CollectionDefinition(nameof(PasswordCollection))]
    public class PasswordCollection : ICollectionFixture<PasswordTestsFixture>
    {
    }

    public class PasswordTestsFixture
    {
        public Mock<IPasswordRepository> PasswordRepositoryMock { get; set; }

        public PasswordAppService GetPasswordAppService()
        {
            var mocker = new AutoMoqer();
            mocker.Create<PasswordAppService>();

            var passwordAppService = mocker.Resolve<PasswordAppService>();

            PasswordRepositoryMock = mocker.GetMock<IPasswordRepository>();

            return passwordAppService;
        }

        public byte[] GenerateSalt(string data)
        {
            List<byte> byteList = new List<byte>();

            string hexPart = data.Substring(2);
            for (int i = 0; i < hexPart.Length / 2; i++)
            {
                string hexNumber = hexPart.Substring(i * 2, 2);
                byteList.Add((byte)Convert.ToInt32(hexNumber, 16));
            }

            return byteList.ToArray();
        }
    }    
}
