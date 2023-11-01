import { useState } from "react";
class InputProps {
    label : string
}

const InputComponent = (props: InputProps) : JSX.Element => {

    const [text, setText] = useState<string>("");

    return (
    <div className="x">
        <label className="xx">{props.label}
            <input className="xxx" value={text} onChange={e => setText(e.target.value)}></input>
        </label>
    </div>
    );
}

export default InputComponent;