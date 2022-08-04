using System;
using System.Text;
using System.Security.Cryptography;

namespace CrudWindowsForm.Servicos
{
    public class SenhaHash
    {
        private static readonly string ChaveUnica = "<RSAKeyValue><Modulus>o8XK8Ljc7Ye0a45339qQ6siw3FgUUDN91SyaTfThp7iiwg5FBIrLl0Ib6E/r+Dyf/gobg5GYGW/lmsNN86Rz5b1riIlOpGwfPwmx3YOT2LnsuzYL5os/+M/WLXXssC742c8pNcCbt2mEaiZqyyDnFsVYGn+nEI/z+i5ruYQZgb4pA3j53Y3Phd9qXOsVLevN/+h2r/Qro7i7dEYkYI0aIiVQrNyDWD95f5cmuWrkMjWUTb5dn3NRknp0WFZIVqQ57Q4EfSdaXQ0h3VL+fpIN7rhv1K9pJkcKdEdiH7AGa5ytA+g3FoGUYdn//x+BpNtpuBeMiNl46xEf7lgZwcqPaQ==</Modulus><Exponent>AQAB</Exponent><P>whAOQBV8MEUENlK57VRwSHVpBuRGDJ92FddsPEwtacPJezH1HIEuIjo+hqLZbQVN8kuXc9nkpk9gnSDDa5liKJqilP+fKCD6ROs8sx6uAgR2d4RB+2HmFWEYJkZfQjIoJs81wKgSW1LYewst7WehpO3wgReaYAp4vcbWc+1qNms=</P><Q>2Argo6L3/7Snd8OrRll99AjDMHu+kb21fRlVgOstsW+OPJrS5W6CAp9gxTSCg9jQ7bRumErOI1iPCVZ78pNrGL4TIxFu1sQMFHkembC/M91Z9BeVxiVZ7JOMgz4davy4Dt+CIKFUS3722IWQsT5o/jqlcmP9Vr94/OYzwOPdvns=</Q><DP>QR1uCqaq4u9leGzNogqhGk77DKrvrjCbSLzdvmW5HzHomwOqQQQ/XJC6hiaCWghbnR8sFF1aUUt1GYPyzlnLC+DHCMO0vZxDHXjpBxkWPesNbRDdquGweB/6IG2gbO+zdUI1wQ0kC6yCotafdHc/T73e4xHuZKde2B6tunxBFT0=</DP><DQ>a5Z3KmaVNF1F7OmfmOgmRmo6siFd1Pxxrjv4jbwQxEU2w02i6yjuS9oM+ghdWsWVvlrVnLfcElQe31qXXUU211qI8zTSStfU4Dk1GPqhLXX1Pw6jwsqFDsCoAA/l3IrAKn/6ML/G19YBnfBeDzZyWPDhxaav2gx+dws5CL0xh5M=</DQ><InverseQ>emR5pVQ8ejrkfeh47UHuK3XxCdMpwvVzvzRjaZHyAMOVnXfVasMDMSD57iMvVbePGEqzYwckBmzrG//11yoHTzdEeU5d91iWLUQIaLUTDaTZgnSStlUHUjz+89j6yOu3p54Y2KVmWcbBrZnFN3LENoNHe3P8xfJ93GG2eTyn9LY=</InverseQ><D>FCJH7co8VvNdCfAM6rdbI5IitCQb8zQxXxjJq5XUfCMl10eb0pAhsWnONoRE1VLWtIuXkFhA47eu+ZTTK4XnPbJI2xNOWMZ2HPDbLgl0Dqvk36soUWKDLdqXUSVzQMG1lEl0nogcXLMbNVWdwXU9E76VM0TITC/2ZaZ5VehYnmuJAiufx1XcGIdvuRfSfkK1Ncbn3bLsomTOO0T2zci4t7EM/1dmBW3BkVnkTfBal9bmubtUQQwUaZBA+VAw343vJ4Wnd46ygRIMaVrCPG8CJKwNgNN6ZwGdWNuvaF0qtl3RKeb0VW/fDttREYzVu0/9aT0cjNWtwRa0AxBJegwEZQ==</D></RSAKeyValue>";

        public static string SenhaCriptografada(string senha)
        {
            using (RSACryptoServiceProvider rsaProvider = new(2048))
            {
                byte[] senhaCriptografada;

                rsaProvider.FromXmlString(ChaveUnica);
                senhaCriptografada = rsaProvider.Encrypt(Encoding.UTF8.GetBytes(senha), true);
                return Convert.ToBase64String(senhaCriptografada);
            }
        }

        public static string SenhaDescriptografada(string senha)
        {
            using (RSACryptoServiceProvider rsaProvider = new(2048))
            {
                byte[] senhaDescriptografada;

                rsaProvider.FromXmlString(ChaveUnica);
                senhaDescriptografada = rsaProvider.Decrypt(Convert.FromBase64String(senha), true);
                return Encoding.UTF8.GetString(senhaDescriptografada);
            }
        }
    }
}
