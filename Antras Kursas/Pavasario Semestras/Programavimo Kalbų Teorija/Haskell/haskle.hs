-- A function to check if a number is an ugly number
isUgly :: Int -> Bool
isUgly 1 = True
isUgly n = any (\p -> n `mod` p == 0) [2, 3, 5] && isUgly (n `div` 2 ^ count 2 n `div` 3 ^ count 3 n `div` 5 ^ count 5 n)
  where count p n = length $ takeWhile (\m -> m `mod` p == 0) $ iterate (`div` p) n

-- A list of the first 1500 ugly numbers
uglyNumbers :: [Int]
uglyNumbers = take 1500 $ filter isUgly [1..]

-- The 1500'th ugly number
answer :: Int
answer = uglyNumbers !! 1499

-- Print the answer
main :: IO ()
main = print answer
