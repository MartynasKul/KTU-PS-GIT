// Example of recording an interaction (customize based on your application)
var userInteractions = [];
function recordUserInteraction(itemId) {
  var userId = "1"; // Replace with the actual user ID
  var interaction = { userId: userId, itemId: itemId };
  userInteractions.push(interaction);
  console.log(userInteractions);
}
function buildUserItemMatrix() {
  var userItemMatrix = {};

  // Check if userInteractions is defined
  if (typeof userInteractions !== "undefined" && userInteractions !== null) {
    // Loop through user interactions
    userInteractions.forEach(function (interaction) {
      var userId = interaction.userId;
      var itemId = interaction.itemId;

      // Initialize user in the matrix if not exists
      if (!userItemMatrix[userId]) {
        userItemMatrix[userId] = {};
      }

      // Set interaction in the matrix
      userItemMatrix[userId][itemId] = 1;
    });
  }

  return userItemMatrix;
}

function calculateItemSimilarities(userItemMatrix) {
  var itemSimilarities = {};

  // Loop through items
  Object.keys(userItemMatrix).forEach(function (itemA) {
    // Initialize similarity array for the item
    itemSimilarities[itemA] = {};

    Object.keys(userItemMatrix).forEach(function (itemB) {
      if (itemA !== itemB) {
        // Calculate Jaccard similarity coefficient
        var intersection = 0;
        var union = 0;

        Object.keys(userItemMatrix[itemA]).forEach(function (user) {
          if (userItemMatrix[itemB][user]) {
            intersection++;
          }
        });

        union =
          Object.keys(userItemMatrix[itemA]).length +
          Object.keys(userItemMatrix[itemB]).length -
          intersection;

        // Jaccard similarity coefficient
        var similarity = intersection / union;

        // Store similarity in the matrix
        itemSimilarities[itemA][itemB] = similarity;
      }
    });
  });

  return itemSimilarities;
}

function suggestItem(userItemMatrix, itemSimilarities) {
  var userId = "1"; // Replace with actual user ID

  // Get items the user has interacted with
  var userItems = Object.keys(userItemMatrix[userId] || {});

  var weightedSums = {};
  var similaritySums = {};

  // Loop through items similar to the ones the user has interacted with
  userItems.forEach(function (userItem) {
    var similarItems = Object.keys(itemSimilarities[userItem] || {});

    similarItems.forEach(function (similarItem) {
      // Exclude items the user has already interacted with
      if (!userItemMatrix[userId][similarItem]) {
        var similarity = itemSimilarities[userItem][similarItem];
        var weight = userItemMatrix[userId][userItem] || 0; // Interaction weight (e.g., 1 for binary interactions)

        // Calculate weighted sum and similarity sum
        weightedSums[similarItem] =
          (weightedSums[similarItem] || 0) + similarity * weight;
        similaritySums[similarItem] =
          (similaritySums[similarItem] || 0) + similarity;
      }
    });
  });

  var scores = {};

  // Calculate recommendation scores only if there are similarity sums
  if (Object.keys(similaritySums).length > 0) {
    // Loop through items
    Object.keys(weightedSums).forEach(function (item) {
      scores[item] =
        similaritySums[item] > 0
          ? weightedSums[item] / similaritySums[item]
          : 0;
    });
  }

  // Find the item with the highest score
  var suggestedItem = Object.keys(scores).reduce(function (a, b) {
    return scores[a] > scores[b] ? a : b;
  }, null);

  // If there are no suggestions based on interactions, suggest an item with the smallest ID
  if (suggestedItem === null) {
    suggestedItem = Object.keys(itemSimilarities).reduce(function (a, b) {
      return parseInt(a, 10) < parseInt(b, 10) ? a : b;
    }, null);
  }

  return suggestedItem;
}

// Function to suggest an item for the user
function suggestItemForUser() {
  // Call buildUserItemMatrix and calculateItemSimilarities
  var userItemMatrix = buildUserItemMatrix();
  var itemSimilarities = calculateItemSimilarities(userItemMatrix);

  // Call suggestItem to get the suggested item
  var suggestedItem = suggestItem(userItemMatrix, itemSimilarities);

  // If there are no suggestions based on interactions or no interactions at all, suggest an item with the smallest ID
  if (suggestedItem === null) {
    var allItemIds = Object.keys(itemSimilarities);
    if (allItemIds.length > 0) {
      suggestedItem = allItemIds.reduce(function (a, b) {
        return parseInt(a, 10) < parseInt(b, 10) ? a : b;
      });
      console.log("Suggested item ID (smallest ID):", 1);
    }
  } else {
    console.log("Suggested item ID:", suggestedItem);
    // You can display the suggested item to the user or perform any desired action
  }
}
