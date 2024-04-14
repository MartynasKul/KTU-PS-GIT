package models;

public class Student  {
    String firstName;
    String lastName;
    int age;
    String group;

    public Student(){}

    public Student(String firstName, String lastName, int age, String group){
        this.firstName = firstName;
        this.lastName = lastName;
        this.age = age;
        this.group = group;
    }

    @Override
    public String toString(){
        return String.format("%s %s age: %d group: %s", firstName, lastName, age, group);
    }

    @Override
    public boolean equals(Object student) {
        if (student == null)
            return false;

        return ((Student) student).firstName.equals(this.firstName) && ((Student) student).lastName.equals(this.lastName);
    }
}
