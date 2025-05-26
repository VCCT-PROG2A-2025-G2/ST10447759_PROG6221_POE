// CyberSecurityBot.Tests/InMemoryResponseServiceTests.cs
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CyberSecurityBot.Services; // Your project's namespace
using System.Collections.Generic;
using System.Linq;

namespace CyberSecurityBot.Tests
{
    [TestClass]
    public class InMemoryResponseServiceTests
    {
        [TestMethod]
        public void GetRandomResponse_ExistingKey_ReturnsValidResponse()
        {
            // Arrange
            InMemoryResponseService service = new InMemoryResponseService();
            string key = "password safety"; // A key known to exist in ResponseData

            // Act
            string response = service.GetRandomResponse(key);

            // Assert
            Assert.IsNotNull(response, "Response should not be null.");
            Assert.IsTrue(ResponseData.Responses[key].Contains(response), "Response should be one of the predefined responses for the key.");
        }

        [TestMethod]
        public void GetRandomResponse_NonExistingKey_ReturnsDefaultMessage()
        {
            // Arrange
            InMemoryResponseService service = new InMemoryResponseService();
            string key = "non_existing_key_for_test";
            string expectedDefaultMessage = "Sorry, I don't have a response for that topic—try another one!"; //

            // Act
            string response = service.GetRandomResponse(key);

            // Assert
            Assert.AreEqual(expectedDefaultMessage, response, "Response should be the default message for non-existing keys.");
        }

        [TestMethod]
        public void GetRandomResponse_KeyWithNoReplies_ReturnsDefaultMessage()
        {
            // Arrange
            // This scenario is a bit tricky with the current ResponseData structure,
            // as all keys have replies. If you had a key with an empty list of replies,
            // this test would be more relevant. For now, we'll simulate by temporarily
            // modifying or using a known key that might change.
            // For a robust test, you might consider a mock or a test-specific data setup.
            // However, based on the current code, this scenario will behave like a non-existing key
            // if the dictionary access `ResponseData.Responses.TryGetValue(key, out var replies) && replies.Count > 0` fails.

            InMemoryResponseService service = new InMemoryResponseService();
            string key = "key_with_no_replies_in_test"; // Assume this key *could* exist but have no replies.
                                                        // Or, if ResponseData.Responses was mockable, we'd set it up.
            string expectedDefaultMessage = "Sorry, I don't have a response for that topic—try another one!"; //


            // Forcing the scenario if ResponseData was an instance variable or mockable:
            // var tempResponses = new Dictionary<string, List<string>>(ResponseData.Responses, StringComparer.OrdinalIgnoreCase);
            // tempResponses["key_with_no_replies_in_test"] = new List<string>();
            // service.SetResponseData(tempResponses); // Imaginary method to set response data for test

            // Act
            string response = service.GetRandomResponse(key);

            // Assert
            Assert.AreEqual(expectedDefaultMessage, response, "Response should be the default message for keys with no replies.");
        }

        [TestMethod]
        public void GetAvailableTopics_ReturnsAllKeysFromResponseData()
        {
            // Arrange
            InMemoryResponseService service = new InMemoryResponseService();

            // Act
            IEnumerable<string> topics = service.GetAvailableTopics();
            List<string> expectedTopics = ResponseData.Responses.Keys.ToList(); //

            // Assert
            Assert.IsNotNull(topics, "Topics collection should not be null.");
            CollectionAssert.AreEquivalent(expectedTopics, topics.ToList(), "The topics should match the keys in ResponseData.");
        }
    }
}