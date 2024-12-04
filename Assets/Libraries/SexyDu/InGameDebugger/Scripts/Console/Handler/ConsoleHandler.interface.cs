namespace SexyDu.InGameDebugger
{
    public interface IConsoleUIHandle
    {
        // 실행여부
        public bool Playing { get; }
        // 로깅 중지 함수
        public void PauseConsole(bool pause);

        // 로그 클리어
        public void ClearConsole();

        // 비활성화
        public void Inactivate();

        // 로그 수집 서브젝트
        public IConsoleLogSubject ConsoleLogSubject { get; }

        // 콘솔 필터 팔로워
        public IConsoleFilterFollower FilterFollower { get; }

    }
}