import { patchTask, deleteTask } from "../api/TaskApi";
import type { ITask, TaskStatus } from "../types/task";
import styles from './TaskCard.module.css';

interface TaskCardProps {
    task: ITask;
    onUpdate: () => void;
}

const statusColors: Record<string, string> = {
  Todo: '#6b7280',
  InProgress: '#f59e0b',
  Done: '#10b981',
};

const priorityColors: Record<string, string> = {
  Low: '#6b7280',
  Medium: '#3b82f6',
  High: '#ef4444',
};

export default function TaskCard({task, onUpdate }: TaskCardProps) {

    const handleStatusChange = async(e: React.ChangeEvent<HTMLSelectElement>) => {
        await patchTask(task.id, { status: e.target.value as TaskStatus });
        onUpdate();
    };

    const handleDelete = async ()=> {
        await deleteTask(task.id);
        onUpdate();
    };

    return (
    <div className={styles.card}>
      <div className={styles.header}>
        <h3 className={styles.title}>{task.title}</h3>
        <span className={styles.priority} style={{ color: priorityColors[task.priority] }}>
          {task.priority}
        </span>
      </div>

      <p className={styles.description}>{task.description}</p>

      <div className={styles.footer}>
        <span className={styles.status} style={{ color: statusColors[task.status] }}>
          {task.status}
        </span>
        <select value={task.status} onChange={handleStatusChange}>
          <option value="Todo">Todo</option>
          <option value="InProgress">InProgress</option>
          <option value="Done">Done</option>
        </select>
        <button className={styles.deleteBtn} onClick={handleDelete}>Delete</button>
      </div>
    </div>
  );
}