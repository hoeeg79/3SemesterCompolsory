using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using DefaultNamespace;
using Infrastructure.Models;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tests : PageTest
{
    [SetUp]
    public void Setup()
    {
        Helper.TriggerRebuild();
    }

    
    [Test]
    public async Task AreThereBoxes()
    {
        var boxes = new List<Box>();
        for (int i = 0; i < 10; i++)
        {
            var box = new Box()
            {
                boxId = i,
                name = "Hello big chonker " + i,
                size = "Kæmpe stor",
                description = "Nu skal Monika til at lave noget haha :)",
                price = 69,
                boxImgUrl = "KæmpeKasser.jpg",
                materials = "Elfenben"
            };
            boxes.Add(box);
            var sql = $@"INSERT INTO 
                    boxes.box (name, size, description, price, boxImage, material)
                    VALUES (@name, @size, @description, @price, @boxImgUrl, @materials);";
            using (var conn = Helper.DataSource.OpenConnection())
            {
                conn.Execute(sql, box);
            }
        }
        
        await Page.GotoAsync("http://localhost:5000/");
        var kasse =  Page.GetByTestId("card_Hello big chonker 8");
        await Expect(kasse).ToBeVisibleAsync();
    }
    
    [Test]
    public async Task CreateBoxTest()
    {
        await Page.GotoAsync("http://localhost:5000/");

        await Page.GetByTestId("create-button").GetByRole(AriaRole.Img).Nth(1).ClickAsync();
        await Page.GetByLabel("insert name for box please").ClickAsync();
        await Page.GetByLabel("insert name for box please").FillAsync("Bajrne");
        await Page.GetByLabel("insert size for the box please").ClickAsync();
        await Page.GetByLabel("insert size for the box please").FillAsync("test");
        await Page.GetByLabel("insert box description please").ClickAsync();
        await Page.GetByLabel("insert box description please").FillAsync("test");
        await Page.GetByLabel("insert cover-image-url for box please").ClickAsync();
        await Page.GetByLabel("insert cover-image-url for box please").FillAsync("test");
        await Page.GetByLabel("insert box price please").ClickAsync();
        await Page.GetByLabel("insert box price please").FillAsync("111");
        await Page.GetByLabel("insert box material please").ClickAsync();
        await Page.GetByLabel("insert box material please").FillAsync("test");
        await Page.GetByRole(AriaRole.Button, new() { Name = "submit" }).ClickAsync();

        await Page.GotoAsync("http://localhost:5000/");

        var kasse = Page.GetByTestId("card_Bajrne");
        await Expect(kasse).ToBeVisibleAsync();
    }

    [Test]
    public async Task CreateBoxFail()
    {
        
        await Page.GotoAsync("http://localhost:5000/");

        await Page.GetByTestId("create-button").GetByRole(AriaRole.Img).Nth(1).ClickAsync();
        await Page.GetByLabel("insert name for box please").ClickAsync();
        await Page.GetByLabel("insert name for box please").FillAsync("name");
        await Page.GetByLabel("insert size for the box please").ClickAsync();
        await Page.GetByLabel("insert size for the box please").FillAsync("test");
        await Page.GetByLabel("insert cover-image-url for box please").ClickAsync();
        await Page.GetByLabel("insert cover-image-url for box please").FillAsync("test");
        await Page.GetByLabel("insert box material please").ClickAsync();
        await Page.GetByLabel("insert box material please").FillAsync("test");

        var submitButton = Page.GetByRole(AriaRole.Button, new() { Name = "submit" });;
        await Expect(submitButton).ToBeDisabledAsync();
    }

    [Test]
    public async Task DeleteBoxes()
    {
        //Arrange
        var box = new Box()
        {
            name = "skalSlettes",
            size = "lille kasse",
            description = "denne kasse er lille bitte",
            price = 99999,
            boxImgUrl = "someurl",
            materials = "gamle tavler"
        };
        var sql = $@"INSERT INTO
                    boxes.box (name, size, description, price, boxImage, material)
                    VALUES (@name, @size, @description, @price, @boxImgUrl, @materials)";
        using (var conn = Helper.DataSource.OpenConnection())
        {
            conn.Execute(sql, box);
        }

        //Act
        await Page.GotoAsync("http://localhost:5000/");
        await Page.GetByTestId("delete-button").ClickAsync();
        //Assert
        var boxGone = Page.GetByTestId("card_skalSlettes");
        await Expect(boxGone).Not.ToBeVisibleAsync();
    }

    [TestCase("chonker 4")]
    [TestCase("chonker 6")]
    public async Task SearchTest(string testCase)
    {
        //Arrange
        var boxes = new List<Box>();
        for (int i = 0; i < 10; i++)
        {
            var box = new Box()
            {
                boxId = i,
                name = "Hello big chonker " + i,
                size = "Kæmpe stor",
                description = "Nu skal Monika til at lave noget haha :)",
                price = 69,
                boxImgUrl = "KæmpeKasser.jpg",
                materials = "Elfenben"
            };
            boxes.Add(box);
            var sql = $@"INSERT INTO 
                    boxes.box (name, size, description, price, boxImage, material)
                    VALUES (@name, @size, @description, @price, @boxImgUrl, @materials);";
            using (var conn = Helper.DataSource.OpenConnection())
            {
                conn.Execute(sql, box);
            }
        }
        
        //Act
        await Page.GotoAsync("http://localhost:5000/");
        await Page.GetByTestId("searchbar").GetByPlaceholder("Enter your search here").ClickAsync();
        await Page.GetByTestId("searchbar").GetByPlaceholder("Enter your search here").FillAsync(testCase);
        
        //Assert
        var kasse =  Page.GetByTestId("card_Hello big " + testCase);
        await Expect(kasse).ToBeVisibleAsync();
    }

    [Test]
    public async Task SearchFailTest()
    {
        //Arrange
        var boxes = new List<Box>();
        for (int i = 0; i < 10; i++)
        {
            var box = new Box()
            {
                boxId = i,
                name = "Hello big chonker " + i,
                size = "Kæmpe stor",
                description = "Nu skal Monika til at lave noget haha :)",
                price = 69,
                boxImgUrl = "KæmpeKasser.jpg",
                materials = "Elfenben"
            };
            boxes.Add(box);
            var sql = $@"INSERT INTO 
                    boxes.box (name, size, description, price, boxImage, material)
                    VALUES (@name, @size, @description, @price, @boxImgUrl, @materials);";
            using (var conn = Helper.DataSource.OpenConnection())
            {
                conn.Execute(sql, box);
            }
        }
        
        //Act
        await Page.GotoAsync("http://localhost:5000/");
        await Page.GetByTestId("searchbar").GetByPlaceholder("Enter your search here").ClickAsync();
        await Page.GetByTestId("searchbar").GetByPlaceholder("Enter your search here").FillAsync("Chonkkers");
        
        //Assert
        var kasse =  Page.GetByTestId("card_Hello big chonker 8");
        await Expect(kasse).Not.ToBeVisibleAsync();
    }

    [Test]
    public async Task UpdateBox()
    {
        //Arrange
        var boxes = new List<Box>();
        for (int i = 0; i < 10; i++)
        {
            var box = new Box()
            {
                boxId = i,
                name = "Hello big chonker " + i,
                size = "Kæmpe stor",
                description = "Nu skal Monika til at lave noget haha :)",
                price = 69,
                boxImgUrl = "KæmpeKasser.jpg",
                materials = "Elfenben"
            };
            boxes.Add(box);
            var sql = $@"INSERT INTO 
                    boxes.box (name, size, description, price, boxImage, material)
                    VALUES (@name, @size, @description, @price, @boxImgUrl, @materials);";
            using (var conn = Helper.DataSource.OpenConnection())
            {
                conn.Execute(sql, box);
            }
        }
        
        //Act
        await Page.GotoAsync("http://localhost:5000/");
        await Page.GetByTestId("card_Hello big chonker 1").GetByRole(AriaRole.Button).Nth(1).ClickAsync();
        await Page.GetByLabel("insert name for box please").ClickAsync();
        await Page.GetByLabel("insert name for box please").FillAsync("Kæmpe lille");
        await Page.GetByRole(AriaRole.Button, new() { Name = "send" }).ClickAsync();
        
        //Assert
        var kasse =  Page.GetByTestId("card_Kæmpe lille");
        await Expect(kasse).ToBeVisibleAsync();
    }
}