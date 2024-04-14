-- Martynas Kuliešius IFF-1/9

-- Funkcija ugly skaiciaus patikrai
isUgly :: Int -> Bool
isUgly 1 = True
isUgly n = any (\p -> n `mod` p == 0) [2, 3, 5] && isUgly (n `div` 2 ^ count 2 n `div` 3 ^ count 3 n `div` 5 ^ count 5 n)
  where count p n = length $ takeWhile (\m -> m `mod` p == 0) $ iterate (`div` p) n

-- Sukuriamas sarasas visu ugly skaiciu
uglyNumbers :: [Int]
uglyNumbers = take 1500 $ filter isUgly [1..]

-- 1500-tasis ugly skaicius
answer :: Int
answer = uglyNumbers !! 1499

-- Atsakymo spausdinimas
main :: IO ()
main = print answer
