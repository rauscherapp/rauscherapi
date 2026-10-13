using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Adapters.Vendors;
using Domain.Models;
using Infrastructure.RateProvider.Providers;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace Application.Tests
{
    public class RateProviderTests
    {
        [Fact]
        public async Task GetRateAsync_CallsEachVendorOnlyOnce()
        {
            var vendorA = new Mock<IVendorRateAdapters>();
            var vendorB = new Mock<IVendorRateAdapters>();
            vendorA.Setup(v => v.GetRateAsync(It.IsAny<List<Symbols>>())).ReturnsAsync(new List<CommoditiesRate>());
            vendorB.Setup(v => v.GetRateAsync(It.IsAny<List<Symbols>>())).ReturnsAsync(new List<CommoditiesRate>());
            var provider = new RateProvider(new[] { vendorA.Object, vendorB.Object }, NullLogger<RateProvider>.Instance);

            await provider.GetRateAsync(new List<Symbols>());

            vendorA.Verify(v => v.GetRateAsync(It.IsAny<List<Symbols>>()), Times.Once);
            vendorB.Verify(v => v.GetRateAsync(It.IsAny<List<Symbols>>()), Times.Once);
        }

        [Fact]
        public async Task GetOpeningRateAsync_CallsEachVendorOnlyOnce()
        {
            var vendor = new Mock<IVendorRateAdapters>();
            vendor.Setup(v => v.GetOpeningRateAsync(It.IsAny<string>(), It.IsAny<List<Symbols>>()))
                  .ReturnsAsync(new List<CommodityOpenHighLowClose>());
            var provider = new RateProvider(new[] { vendor.Object }, NullLogger<RateProvider>.Instance);

            await provider.GetOpeningRateAsync("2026-10-13", new List<Symbols>());

            vendor.Verify(v => v.GetOpeningRateAsync(It.IsAny<string>(), It.IsAny<List<Symbols>>()), Times.Once);
        }

        [Fact]
        public async Task GetRateAsync_IgnoresVendorThatReturnsNull()
        {
            var vendor = new Mock<IVendorRateAdapters>();
            vendor.Setup(v => v.GetRateAsync(It.IsAny<List<Symbols>>())).ReturnsAsync((IEnumerable<CommoditiesRate>)null);
            var provider = new RateProvider(new[] { vendor.Object }, NullLogger<RateProvider>.Instance);

            var result = await provider.GetRateAsync(new List<Symbols>());

            Assert.Empty(result);
        }
    }
}
