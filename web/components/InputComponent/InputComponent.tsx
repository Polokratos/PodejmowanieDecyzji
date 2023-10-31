import { useState } from "react";
import styles from "./InputComponent.module.css";

class InputProps {
    label : string
}

const InputComponent = (props: InputProps) : JSX.Element => {

    const [text, setText] = useState<string>("");

    return (
    <div className={styles.component}>
        <label className={styles.label}>{props.label}
            <input className={styles.input} value={text} onChange={e => setText(e.target.value)}></input>
        </label>
    </div>
    );
}

export default InputComponent;