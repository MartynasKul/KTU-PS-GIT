package com.company;
import util.*;

import java.util.ArrayDeque;
import java.util.ArrayList;
import java.util.Deque;

public class Test {

    public static boolean isBalanced(String line){
        LinkedListStack<Character> stack = new LinkedListStack<Character>();

        if(line.length() == 0)
            return false;

        for(int i = 0; i < line.length(); i++){
            char current = line.charAt(i);
            if(current == '{' || current == '(' || current == '['){
                stack.push(current);
            }
            if(current == '}' || current == ')' || current == ']'){
                if(stack.isEmpty())
                    return false;
                char stackTop = stack.peak();

                if(stackTop == '{' && current == '}' ||
                    stackTop == '(' && current == ')' ||
                    stackTop == '[' && current == ']'){
                    stack.pop();
                }else{
                    return false;
                }
            }
        }

        if(stack.isEmpty())
            return true;
        return false;
    }

    public static ArrayList findMax(ArrayList<Integer> numbers, int k) {

        ArrayList<Integer> maxValues = new ArrayList<>();
        ArrayDeque<Integer> deque = new ArrayDeque<>();

        for(int i = 0; i < numbers.size(); i++){
            if(!deque.isEmpty() && deque.peek() < i - k + 1){
                deque.poll();
            }
            while (!deque.isEmpty() && numbers.get(deque.peekLast()) < numbers.get(i)) {
                deque.pollLast();
            }
            deque.offer(i);
            if (i >= k - 1) {
                maxValues.add(numbers.get(deque.peek()));
            }
        }
        return maxValues;
    }

}
