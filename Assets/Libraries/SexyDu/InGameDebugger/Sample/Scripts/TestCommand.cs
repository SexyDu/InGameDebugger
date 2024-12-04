using System;
using System.Text;
using UnityEngine;

namespace SexyDu.InGameDebugger.Sample
{
    /// <summary>
    /// [샘플] CLI 명령 관리 예제
    /// </summary>
    public class TestCommand : MonoBehaviour
    {
        private ICommandContainer CommandContainer => InGameDebuggerConfig.Ins.CommandContainer;

        [SerializeField] private string[] commandDatas;
        [SerializeField] private int commandValue;

        /// <summary>
        /// 명령 등록
        /// </summary>
        public void Bind()
        {
            CommandContainer.Bind("ExecuteTestCommand", (parameters) => {
                Debug.Log("[CMD: ExecuteTestCommand] Command가 실행되었습니다.");
            });

            CommandContainer.Bind("SetCommandDatas", (parameters) => {
                if (parameters != null)
                {
                    commandDatas = parameters;
                    Debug.Log("[CMD: SetCommandDatas] CommandData가 설정되었습니다.");
                }
                else
                {
                    Debug.LogWarning("[CMD: SetCommandDatas] CommandData가 없습니다.");
                }
            });

            CommandContainer.Bind("SetCommandValue", (parameters) => {

                if (parameters == null || parameters.Length.Equals(0))
                {
                    Debug.LogWarning("[CMD: SetCommandValue] 값이 없습니다.");
                }
                else
                {
                    try
                    {
                        commandValue = Convert.ToInt32(parameters[0]);

                        Debug.Log("[CMD: SetCommandValue] CommandValue가 설정되었습니다.");
                    }
                    catch(FormatException)
                    {
                        throw new FormatException($"전달값은 값({parameters[0]})을 int형으로 변환할 수 없습니다.");
                    }
                    catch(OverflowException)
                    {
                        throw new OverflowException($"전달받은 값({parameters[0]})이 int형의 범위를 벗어났습니다.");
                    }
                }
            });

            CommandContainer.Bind("LogDatas", (parameters) => {
                LogDatas();
            });

            CommandContainer.Bind("LogValue", (parameters) => {
                LogValue();
            });
        }

        /// <summary>
        /// 명령 해제
        /// </summary>
        public void Unbind()
        {
            CommandContainer.Unbind("ExecuteTestCommand");
            CommandContainer.Unbind("SetCommandDatas");
            CommandContainer.Unbind("SetCommandValue");
            CommandContainer.Unbind("LogDatas");
            CommandContainer.Unbind("LogValue");
        }

        /// <summary>
        /// commandDatas 로깅
        /// </summary>
        private void LogDatas()
        {
            if (commandDatas == null || commandDatas.Length.Equals(0))
            {
                Debug.Log("TestCommand.commandDatas : [Empty]");
            }
            else
            {
                StringBuilder sb = new StringBuilder(commandDatas[0]);
                for (int i = 1; i < commandDatas.Length; i++)
                {
                    sb.AppendFormat(", {0}", commandDatas[i]);
                }

                Debug.LogFormat("TestCommand.commandDatas : {0}", sb.ToString());
            }
        }

        /// <summary>
        /// commandDatas 로깅
        /// </summary>
        private void LogValue()
        {
            Debug.LogFormat("TestCommand.cammandValue : {0}", commandValue);
        }
    }
}