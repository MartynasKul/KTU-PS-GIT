package edu.ktu.ds.lab2.demo;

import org.junit.Assert;
import org.junit.Test;

public class ExampleTests {

    @Test
    public void assertExample() {
        long expected = 4L;
        long actual = 4L;
        Assert.assertEquals(expected, actual);
    }
}
