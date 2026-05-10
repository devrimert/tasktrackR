import { useState } from "react";
import CreateTaskForm from "./components/CreateTaskForm";
import TaskList from "./components/TaskList";
import styles from './App.module.css'

export default function App(){
  const [refreshKey, setRefreshKey] = useState(0);

  const handleRefresh = () => {
    setRefreshKey(prev => prev+1);
  };

  return (
    <div className={styles.container}>
      <header className={styles.header}>
        <h1 className={styles.logo}>TaskTrackR</h1>
      </header>
      <main className={styles.main}>
        <CreateTaskForm onCreated={handleRefresh} />
        <TaskList key={refreshKey} />
      </main>
    </div>
  );

}