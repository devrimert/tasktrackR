import axios from "axios";
import type { ITask, ICreateTask } from "../types/task";

const api = axios.create({
    baseURL: import.meta.env.VITE_API_URL ?? 'http://localhost:5174',
});

export const getTasks = async (): Promise<ITask[]> => {
    const {data} = await api.get('/api/tasks');
    return data;
};

export const createTask = async (task: ICreateTask): Promise<ITask> => {
    const {data} = await api.post('/api/tasks', task);
    return data;
};

export const updateTaskStatus = async (id: number, status: string): Promise<ITask> => {
    const {data} = await api.put(`/api/tasks/${id}`, {status: status});
    return data;
}

export const patchTask = async (id: number, changes: Partial<ITask>): Promise<ITask> => {
    const {data} = await api.patch(`/api/tasks/${id}`, changes);
    return data;
}

export const deleteTask = async (id:number): Promise<void> => {
    await api.delete(`api/tasks/${id}`);
}