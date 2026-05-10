import React, { useState } from "react";
import type { TaskPriority } from "../types/task";
import { createTask } from "../api/TaskApi";
import styles from './CreateTaskForm.module.css';

interface CreateTaskFormProps {
    onCreated: () => void;
}

export default function CreateTaskForm({ onCreated}: CreateTaskFormProps) {
    const [title, setTitle] = useState('');
    const [description, setDescription] = useState('');
    const [priority, setPriority] = useState<TaskPriority>('Medium');

    const handleSubmit = async (e: React.SyntheticEvent) => {
        e.preventDefault();
        await createTask({title, description, priority});
        setTitle('');
        setDescription('');
        setPriority('Medium');
        onCreated();
    };
    return (
        <form className={styles.form} onSubmit={handleSubmit}>
          <h2 className={styles.title}>New Task</h2>

          <div className={styles.field}>
            <input
              className={styles.input}
              type="text"
              placeholder="Title"
              value={title}
              onChange={e => setTitle(e.target.value)}
              required
            />
          </div>

          <div className={styles.field}>
            <input
              className={styles.input}
              type="text"
              placeholder="Description"
              value={description}
              onChange={e => setDescription(e.target.value)}
              />
          </div>

          <div className={styles.field}>
            <select
              className={styles.select}
              value={priority}
              onChange={e => setPriority(e.target.value as TaskPriority)}
            >
              <option value="Low">Low</option>
              <option value="Medium">Medium</option>
              <option value="High">High</option>
            </select>
          </div>

          <button className={styles.submitBtn} type="submit">Create</button>
        </form>
    );
}