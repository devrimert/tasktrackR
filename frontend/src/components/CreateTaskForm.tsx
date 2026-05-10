import React, { useState } from "react";
import type { TaskPriority } from "../types/task";
import { createTask } from "../api/TaskApi";
import styles from './CreateTaskForm.module.css';
import ErrorMessage from './ErrorMessage';

interface CreateTaskFormProps {
    onCreated: () => void;
}

export default function CreateTaskForm({ onCreated}: CreateTaskFormProps) {
    const [title, setTitle] = useState('');
    const [description, setDescription] = useState('');
    const [priority, setPriority] = useState<TaskPriority>('Medium');
    const [errors, setErrors] = useState<string[]>([]);

    const handleSubmit = async (e: React.SyntheticEvent) => {
        e.preventDefault();
        setErrors([]);

        if(!title.trim()) {
          setErrors(['Title is required.']);
          return;
        }

        try{
          await createTask({title, description, priority});
          setTitle('');
          setDescription('');
          setPriority('Medium');
          onCreated();
        } catch (err:any) {
          const data = err?.response?.data;

          if (data?.errors) {
            const allErrors = Object.values(data.errors).flat() as string[];
            setErrors(allErrors);
          } else {
            setErrors([data?.error ?? 'Something went wrong.']);
          }
        }

        
    };
    return (
        <form className={styles.form} onSubmit={handleSubmit}>
          <h2 className={styles.title}>New Task</h2>

           <ErrorMessage errors={errors} />

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