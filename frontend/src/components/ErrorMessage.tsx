import styles from './ErrorMessage.module.css';

interface ErrorMessageProps {
    errors: string[];
}

export default function ErrorMessage({ errors }: ErrorMessageProps) {
    if (errors.length === 0) return null;

    return (
        <div className={styles.box}>
            <ul className={styles.list}>
                {errors.map((err, i) => (
                    <li className={styles.item} key={i}>{err}</li>
                ))}
            </ul>
        </div>
    );
}