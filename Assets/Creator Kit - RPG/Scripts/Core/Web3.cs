using System.Threading.Tasks;
using Thirdweb;
using UnityEngine;

namespace RPGM.Core
{
    public class Web3 : MonoBehaviour
    {
        private ThirdwebSDK sdk = new ThirdwebSDK("optimism-goerli");

        public async Task<bool> IsConnected()
        {
            return await sdk.wallet.IsConnected();
        }

        public async Task<string> Connect()
        {
            string addr = await sdk.wallet.Connect();
            sdk.wallet.SwitchNetwork(420);
            return addr;
        }

        public Contract GetTokenDropContract()
        {
            return sdk
                .GetContract("0x07E29106198B3b43Ada9A833Aee3e7CE74D38446");
        }

        public async Task<TransactionResult[]> Claim()
        {
            var contract = GetTokenDropContract();

            await Connect();

            return await contract.ERC20.Claim("10");
        }

        public Marketplace GetMarketplaceContract()
        {
            return sdk
                .GetContract("0x641c81F8c10e2958F4e0c00882014c0A3A03f86A")
                .marketplace;
        }

        public async Task<TransactionResult> BuyItem(string itemId)
        {
            await Connect();
            return await GetMarketplaceContract().BuyListing(itemId, 1);
        }

        internal async Task<string> GetAddress()
        {
            if (Application.isEditor)
            {
                return "0x0000000000000000000000000000000000000000";
            }
            return await sdk.wallet.GetAddress();
        }
    }
}
