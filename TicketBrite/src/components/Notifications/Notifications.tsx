import { toast, ToastContentProps } from "react-toastify";

interface Props {
    text: string;
};

interface WarningProp {
    text: string;
    onConfirm: () => void;
    onCancel: () => void;
};

export function SuccessNotification({ text }: Props) {
    return toast.success(text, {
        position: "top-right",
        autoClose: 5000,
        hideProgressBar: false,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
        toastId: 1
    });
}

export function ErrorNotification({ text }: Props) {
    return toast.error(text, {
        position: "top-right",
        autoClose: 5000,
        hideProgressBar: false,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
        toastId: 2
    });
}

export function WarningNotification({ text, onConfirm, onCancel }: WarningProp) {
    return toast(
        (props: ToastContentProps) => (
            <div>
                <p>{text}</p>
                <div style={{ display: "flex", justifyContent: "space-between", marginTop: "10px" }}>
                    <button
                        onClick={() => {
                            onConfirm();
                            props.closeToast?.(); // Sluit de melding
                        }}
                        className="btn btn-success"
                        data-test="confirm-delete-item"
                    >
                        Ja
                    </button>
                    <button
                        onClick={() => {
                            onCancel();
                            props.closeToast?.(); // Sluit de melding
                        }}
                        className="btn btn-danger"
                         data-test="cancel-delete-item"
                    >
                        Nee
                    </button>
                </div>
            </div>
        ),
        {
            autoClose: false,
            closeOnClick: false,
            position: "top-right",
            toastId: 3
        }
    );
}
