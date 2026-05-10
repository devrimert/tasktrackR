import { useEffect, useState } from "react";
import type { ITask } from "../types/task";
import { getTasks } from "../api/TaskApi";
import TaskCard from "./TaskCard";
import styles from './TaskList.module.css'

export default function TaskList(){
    const [tasks, setTasks] = useState<ITask[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState('');

    const fetchTasks = async () => {
        try {
          setLoading(true);
          const data = await getTasks();
          console.log('API response:', data); // ← bunu ekle
          setTasks(Array.isArray(data) ? data : []);
        } catch (err) {
          console.error('Error:', err);
          setError('Failed to load tasks');
        } finally {
          setLoading(false);
        }
  };

    useEffect(() => {
        fetchTasks();
    }, []);

    if (loading) return <p className={styles.loading}>Loading...</p>;
    if (error) return <p className={styles.error}>{error}</p>;
    if (tasks.length===0) return <p className={styles.empty}>No tasks yet. Create one above!</p>;

    return (
    <div className={styles.container}>
      {tasks.map(task => (
        <TaskCard key={task.id} task={task} onUpdate={fetchTasks} />
      ))}
    </div>
  );

}