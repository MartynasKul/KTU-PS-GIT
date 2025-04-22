
function extractChannelInfo(str) {

//   const prefixLLP = str.match(/^([A-Za-z]{2})\|\s*([^|]+)\s*\|([^|]*)/);
  
      const prefixLLP = str.trim().match(/([A-Za-z]{2})\|\s*([^]+)/);

  
  if (prefixLLP) {
      console.log('Regex tested:', prefixLLP)
      console.log('Now extra results:')
    console.log("Full match:", prefixLLP[0]);
    console.log("Group 1 (country code):", prefixLLP[1]);
    console.log("Group 2 (channel name):", prefixLLP[2]);
    // console.log("Group 3 (suffix):", prefixLLP[3]);
    
    return {
      strWithoutPrefix: prefixLLP[2].trim(),
      prefix: prefixLLP[1] + "|",
    //   suffix: prefixLLP[3].trim()
    };
  } else {
    return {
      strWithoutPrefix: str,
      prefix: "",
      suffix: ""
    };
  }
}

// Test examples
const testCases = [
  "DE| MAGENTA SPORT 1 |Live Events",
  "PT| ELEVEN 6 |",
  "8ᴋ| CANAL COCINA |ES",
  "Regular Channel Without Prefix",
  "DE| MAGENTA SPORT 11 |Live Events"
];

// Test each case
testCases.forEach((testStr, index) => {
  console.log(`\nTest Case ${index + 1}: "${testStr}"`);
  const result = extractChannelInfo(testStr);
  console.log("Result:", result);
});
