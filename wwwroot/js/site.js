// This file contains JavaScript functionality for the Task Planner application.
// It includes functions to handle task operations such as adding, removing, and marking tasks as completed.

document.addEventListener('DOMContentLoaded', function () {
    const taskList = document.getElementById('taskList');

    // Function to add a task
    document.getElementById('addTaskForm').addEventListener('submit', function (event) {
        event.preventDefault();
        const taskName = document.getElementById('taskName').value;
        const taskDescription = document.getElementById('taskDescription').value;
        const taskDueDate = document.getElementById('taskDueDate').value;

        // Logic to add the task (e.g., send to server or update UI)
        // This is a placeholder for actual implementation
        const newTask = document.createElement('li');
        newTask.textContent = `${taskName} - ${taskDescription} (Due: ${taskDueDate})`;
        taskList.appendChild(newTask);

        // Clear the form
        document.getElementById('addTaskForm').reset();
    });

    // Function to remove a task
    taskList.addEventListener('click', function (event) {
        if (event.target.tagName === 'LI') {
            taskList.removeChild(event.target);
        }
    });

    // Function to mark a task as completed
    taskList.addEventListener('dblclick', function (event) {
        if (event.target.tagName === 'LI') {
            event.target.classList.toggle('completed');
        }
    });
});