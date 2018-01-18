using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace RK.IdentityServer4.OAuth2
{
    public class OAuth2Config
    {
        //定义系统中的资源
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                    new IdentityResources.OpenId(),
                    new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("rk", "Round King API"),                         
            };
        }

        // client want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            // client credentials client
            return new List<Client>
            {
                //Client Credentials模式
                new Client
                {
                    //client_id
                    ClientId = "credt_client",
                    AllowedGrantTypes = new string[] { GrantType.ClientCredentials },
                    //client_secret
                    ClientSecrets =
                    {
                        new Secret("credt_secret".Sha256())
                    },
                    //scope
                    AllowedScopes =
                    {
                        "rk",
                        //Client Credentials模式不支持RefreshToken的，因此不需要设置OfflineAccess
                        //StandardScopes.OfflineAccess.Name,
                    },
                },
                //Resource Owner Password模式
                new Client
                {
                    //client_id
                    ClientId = "pwd_client",
                    AllowedGrantTypes = new string[] { GrantType.ResourceOwnerPassword },
                    //client_secret
                    ClientSecrets =
                    {
                        new Secret("pwd_secret".Sha256())
                    },
                    //scope
                    AllowedScopes =
                    {
                        "rk",
                        //如果想带有RefreshToken，那么必须设置：StandardScopes.OfflineAccess
                        StandardScopes.OfflineAccess,
                        StandardScopes.OpenId,
                        StandardScopes.Profile
                    },
                    AllowOfflineAccess = true,  //如果想带有RefreshToken，那么必须设置：StandardScopes.OfflineAccess
                    AccessTokenLifetime = 3600, //AccessToken的过期时间， in seconds (defaults to 3600 seconds / 1 hour)
                    //AbsoluteRefreshTokenLifetime = 60, //RefreshToken的最大过期时间，就算你使用了TokenUsage.OneTimeOnly模式，更新的RefreshToken最大期限也是为这个属性设置的(就是6月30日就得要过期[根据服务器时间]，你用旧的RefreshToken重新获取了新RefreshToken，新RefreshToken过期时间也是6月30日)， in seconds. Defaults to 2592000 seconds / 30 day
                    //RefreshTokenUsage = TokenUsage.OneTimeOnly,   //默认状态，RefreshToken只能使用一次，使用一次之后旧的就不能使用了，只能使用新的RefreshToken
                    //RefreshTokenUsage = TokenUsage.ReUse,   //重复使用RefreshToken，RefreshToken过期了就不能使用了
                }
            };
        }        
    }
}
